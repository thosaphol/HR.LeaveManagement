using System;
using MediatR;
    using HR.LeaveManagement.Application.Features.LeaveAllowcations.Requests.Commands;
    using AutoMapper;

    using HR.LeaveManagement.Domain;
    using System.Threading.Tasks;
    using System.Threading;
    using HR.LeaveManagement.Application.DTOs.LeaveAllocation.Validators;
    using FluentValidation;
    using FluentValidation.Results;
    using HR.LeaveManagement.Application.Responses;
using HR.LeaveManagement.Application.Contracts.Persistance;
using HR.LeaveManagement.Application.Contracts.Identity;
using System.Collections.Generic;

namespace HR.LeaveManagement.Application.Features.LeaveAllowcations.Handlers.Commands
{
    
    public class CreateLeaveAllocationCommandHandler : IRequestHandler<CreateLeaveAllocationCommand, BaseCommandResponse>
    {
        private readonly ILeaveAllocationRepository _leaveAllocationRepository;
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly IUserService   _userServices;
        private readonly IMapper _mapper;

        public CreateLeaveAllocationCommandHandler(ILeaveAllocationRepository leaveAllocationRepository,
                                                    ILeaveTypeRepository leaveTypeRepository, IUserService userServices,
                                                    IMapper mapper)
        {
            _leaveAllocationRepository = leaveAllocationRepository;
            _leaveTypeRepository = leaveTypeRepository;
            _userServices = userServices;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateLeaveAllocationCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await ValidateRequest(request);
            if (!validationResult.IsValid)
            {
                return new BaseCommandResponse
                {
                    Success = false,
                    Message = "Creation Failed",
                    Errors = validationResult.Errors.ConvertAll(err => err.ErrorMessage)
                };
            }
            var leaveType = await _leaveTypeRepository.Get(request.LeaveAllocationDto.LeaveTypeId);
            var period = DateTime.Now.Year;
            var employees = await _userServices.GetEmployees();
            var allocations = new List<LeaveAllocation>();
            foreach (var emp in employees)
            {
                if (await _leaveAllocationRepository.AllocationExists(emp.Id, leaveType.Id, period))
                    continue;
                allocations.Add(new LeaveAllocation
                {
                    EmployeeId = emp.Id,
                    LeaveTypeId = leaveType.Id,
                    Period = period,
                    NumberOfDays = leaveType.DefaultDays
                });
            }

            await _leaveAllocationRepository.AddAllocations(allocations);
            // var leaveAllocation = _mapper.Map<LeaveAllocation>(request.LeaveAllocationDto);
            // leaveAllocation = await _leaveAllocationRepository.Add(leaveAllocation);
            // return leaveAllocation.Id;
            return new BaseCommandResponse
            {
                Success = true,
                Message = "Creation successful",
                Id = leaveType.Id,
            };
        }
        private async Task<ValidationResult> ValidateRequest(CreateLeaveAllocationCommand request)
        {
            var validator = new CreateLeaveAllocationDtoValidator(_leaveTypeRepository);
            var validationResult = await validator.ValidateAsync(request.LeaveAllocationDto);
            return validationResult;
        }
        
    }
}
