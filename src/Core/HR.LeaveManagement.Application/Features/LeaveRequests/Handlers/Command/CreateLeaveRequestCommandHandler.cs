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

namespace HR.LeaveManagement.Application.Features.LeaveRequests.Handlers.Command
{


    public class CreateLeaveRequestCommandHandler : IRequestHandler<CreateLeaveRequestCommand, BaseCommandResponse>
    {
        private readonly ILeaveRequestRepository _leaveRequestRepository;
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly IEmailSender _emailSender;
        private readonly IMapper _mapper;

        public CreateLeaveRequestCommandHandler(ILeaveRequestRepository leaveRequestRepository,ILeaveTypeRepository leaveTypeRepository,IEmailSender emailSender, IMapper mapper)
        {
            _leaveRequestRepository = leaveRequestRepository;
            _leaveTypeRepository = leaveTypeRepository;
            _emailSender = emailSender;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateLeaveRequestCommand request, CancellationToken cancellationToken)
        {
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
            leaveRequest = await _leaveRequestRepository.Add(leaveRequest);
            try
            {
                await _emailSender.SendEmail(new Email
                {
                    To = "thosaphol@outlook.co.th",
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
