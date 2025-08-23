using System;

namespace HR.LeaveManagement.Application.Features.LeaveAllowcations.Handlers.Commands
{
    using MediatR;
    using HR.LeaveManagement.Application.Features.LeaveAllowcations.Requests.Commands;
    using AutoMapper;

    using HR.LeaveManagement.Domain;
    using HR.LeaveManagement.Application.Persistance.Contracts;
    using System.Threading.Tasks;
    using System.Threading;
    using HR.LeaveManagement.Application.DTOs.LeaveAllocation.Validators;
    using FluentValidation;
    using FluentValidation.Results;
    using HR.LeaveManagement.Application.Responses;

    public class CreateLeaveAllocationCommandHandler : IRequestHandler<CreateLeaveAllocationCommand, BaseCommandResponse>
    {
        private readonly ILeaveAllocationRepository _leaveAllocationRepository;
        private readonly IMapper _mapper;

        public CreateLeaveAllocationCommandHandler(ILeaveAllocationRepository leaveAllocationRepository, IMapper mapper)
        {
            _leaveAllocationRepository = leaveAllocationRepository;
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
            var leaveAllocation = _mapper.Map<LeaveAllocation>(request.LeaveAllocationDto);
            leaveAllocation = await _leaveAllocationRepository.Add(leaveAllocation);
            // return leaveAllocation.Id;
            return new BaseCommandResponse
            {
                Success = true,
                Message = "Creation successful",
                Id = leaveAllocation.Id,
            };
        }
        private async Task<ValidationResult> ValidateRequest(CreateLeaveAllocationCommand request)
        {
            var validator = new CreateLeaveAllocationDtoValidator(_leaveAllocationRepository);
            var validationResult = await validator.ValidateAsync(request.LeaveAllocationDto);
            return validationResult;
        }
        
    }
}
