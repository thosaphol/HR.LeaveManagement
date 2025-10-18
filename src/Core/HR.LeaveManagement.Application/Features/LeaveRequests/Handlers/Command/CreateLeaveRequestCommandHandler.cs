using System;
using MediatR;
using AutoMapper;
using HR.LeaveManagement.Application.Features.LeaveRequests.Requests.Commands;
using HR.LeaveManagement.Application.DTOs.LeaveRequest;
using HR.LeaveManagement.Domain;
using System.Threading.Tasks;
using System.Threading;
using HR.LeaveManagement.Application.DTOs.LeaveRequest.Validators;
using HR.LeaveManagement.Application.Exceptions;
using FluentValidation.Results;
using HR.LeaveManagement.Application.Responses;
using System.Linq;
using HR.LeaveManagement.Application.Contracts.Persistance;
using HR.LeaveManagement.Application.Models;
using HR.LeaveManagement.Application.Contracts.Infrastructure;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace HR.LeaveManagement.Application.Features.LeaveRequests.Handlers.Command
{


    public class CreateLeaveRequestCommandHandler : IRequestHandler<CreateLeaveRequestCommand, BaseCommandResponse>
    {
        private readonly ILeaveRequestRepository _leaveRequestRepository;
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly ILeaveAllocationRepository _leaveAllocationRepository;
        private readonly IEmailSender _emailSender;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public CreateLeaveRequestCommandHandler(ILeaveRequestRepository leaveRequestRepository,
        ILeaveTypeRepository leaveTypeRepository,
        ILeaveAllocationRepository leaveAllocationRepository,
        IEmailSender emailSender,
        IHttpContextAccessor httpContextAccessor,
        IMapper mapper)
        {
            _leaveRequestRepository = leaveRequestRepository;
            _leaveTypeRepository = leaveTypeRepository;
            _emailSender = emailSender;
            _httpContextAccessor = httpContextAccessor;
            _leaveAllocationRepository = leaveAllocationRepository;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateLeaveRequestCommand request, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(q => q.Type == "uid")?.Value;
            var validationResult = await ValidateRequest(request);
            if (!validationResult.IsValid)
            {
                return new BaseCommandResponse
                {
                    Success = false,
                    Message = "Creation Failed",
                    Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }

            var allocation = await _leaveAllocationRepository.GetUserAllocations(userId, request.LeaveRequestDto.LeaveTypeId);
            var daysRequested = (int)(request.LeaveRequestDto.EndDate - request.LeaveRequestDto.StartDate).TotalDays;

            if (allocation is null || allocation.NumberOfDays < daysRequested)
            {
                validationResult.Errors.Add(new ValidationFailure(
                        nameof(request.LeaveRequestDto.EndDate), "You do not have enough days for this request"));
            }
            
            var leaveType = await _leaveTypeRepository.Get(request.LeaveRequestDto.LeaveTypeId);
            if(leaveType == null)
            {
                return new BaseCommandResponse
                {
                    Success = false,
                    Message = "Leave Type Does Not Exist",
                };
            }
            // request.LeaveRequestDto.StartDate = _mapper.Map<LeaveTypeDto>(leaveType);
            var leaveRequest = _mapper.Map<Domain.LeaveRequest>(request.LeaveRequestDto);
            leaveRequest.RequestingEmployeeId = userId;
            leaveRequest = await _leaveRequestRepository.Add(leaveRequest);
            try
            {
                var emailAddress = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;
                await _emailSender.SendEmail(new Email
                {
                    To = emailAddress,
                    Body = $"Your leave request for {request.LeaveRequestDto.StartDate:D} to {request.LeaveRequestDto.EndDate:D} has been submitted successfully.",
                    
                    Subject = "Leave Request Submitted"});
            }
            catch (Exception ex){
                // throw;
            }


            // return leaveRequest.Id;
            return new BaseCommandResponse
            {
                Success = true,
                Message = "Create successful",
                Id = leaveRequest.Id,
            };
        }

        private async Task<ValidationResult> ValidateRequest(CreateLeaveRequestCommand request)
        {
            var validator = new CreateLeaveRequestDtoValidator(_leaveRequestRepository);
            var validationResult = await validator.ValidateAsync(request.LeaveRequestDto);
            return validationResult;
            // if (!validationResult.IsValid)
            // {
            //     throw new ValidationException(validationResult);
            // }
        }
    }
}
