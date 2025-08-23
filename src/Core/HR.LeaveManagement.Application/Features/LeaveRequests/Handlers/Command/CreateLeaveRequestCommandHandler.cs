using System;

namespace HR.LeaveManagement.Application.Features.LeaveRequests.Handlers.Command
{
    using MediatR;
    using AutoMapper;
    using HR.LeaveManagement.Application.Features.LeaveRequests.Requests.Commands;
    using HR.LeaveManagement.Application.DTOs.LeaveRequest;
    using HR.LeaveManagement.Domain;
    using HR.LeaveManagement.Application.Persistance.Contracts;
    using System.Threading.Tasks;
    using System.Threading;
    using HR.LeaveManagement.Application.DTOs.LeaveRequest.Validators;
    using HR.LeaveManagement.Application.Exceptions;
    using FluentValidation.Results;
    using HR.LeaveManagement.Application.Responses;
    using System.Linq;

    public class CreateLeaveRequestCommandHandler : IRequestHandler<CreateLeaveRequestCommand, BaseCommandResponse>
    {
        private readonly ILeaveRequestRepository _leaveRequestRepository;
        private readonly IMapper _mapper;

        public CreateLeaveRequestCommandHandler(ILeaveRequestRepository leaveRequestRepository, IMapper mapper)
        {
            _leaveRequestRepository = leaveRequestRepository;
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
            var leaveRequest = _mapper.Map<LeaveRequest>(request.LeaveRequestDto);
            leaveRequest = await _leaveRequestRepository.Add(leaveRequest);
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
