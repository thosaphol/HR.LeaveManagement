using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation.Results;
using HR.LeaveManagement.Application.DTOs.LeaveType.Validators;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Features.LeaveTypes.Requests.Commande;
using HR.LeaveManagement.Application.Persistance.Contracts;
using HR.LeaveManagement.Application.Responses;
using HR.LeaveManagement.Domain;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveTypes.Handlers.Commands
{
    public class CreateLeaveTypeCommandHandler : IRequestHandler<CreateLeaveTypeCommand, BaseCommandResponse>
    {
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly IMapper _mapper;

        public CreateLeaveTypeCommandHandler(ILeaveTypeRepository leaveTypeRepository, IMapper mapper)
        {
            _leaveTypeRepository = leaveTypeRepository;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateLeaveTypeCommand request, CancellationToken cancellationToken)
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
            var leaveType = _mapper.Map<LeaveType>(request.LeaveTypeDto);

            await _leaveTypeRepository.Add(leaveType);
            // return leaveType.Id;
            return new BaseCommandResponse
            {
                Success = true,
                Message = "Creation successful",
                Id = leaveType.Id,
            };
        }
        private async Task<ValidationResult> ValidateRequest(CreateLeaveTypeCommand request)
        {
            var validator = new CreateLeaveTypeDtoValidatore();
            var validationResult = await validator.ValidateAsync(request.LeaveTypeDto);
            return validationResult;
            // if (!validationResult.IsValid)
            // {
            //     throw new ValidationException(validationResult);
            // }
        }
    }
}
