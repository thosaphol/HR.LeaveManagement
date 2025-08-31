using System;
using MediatR;
using AutoMapper;
using HR.LeaveManagement.Application.Features.LeaveAllowcations.Requests.Commands;
using System.Threading;
using System.Threading.Tasks;
using HR.LeaveManagement.Application.DTOs.LeaveAllocation.Validators;
using FluentValidation;
using HR.LeaveManagement.Application.Contracts.Persistance;
namespace HR.LeaveManagement.Application.Features.LeaveAllowcations.Handlers.Commands
{


    public class UpdateLeaveAllocationCommandHandler : IRequestHandler<UpdateLeaveAllocationCommand, Unit>
    {
        private readonly ILeaveAllocationRepository _leaveAllocationRepository;
        private readonly IMapper _mapper;

        public UpdateLeaveAllocationCommandHandler(ILeaveAllocationRepository leaveAllocationRepository, IMapper mapper)
        {
            _leaveAllocationRepository = leaveAllocationRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateLeaveAllocationCommand request, CancellationToken cancellationToken)
        {
            await ValidateRequest(request);
            var leaveAllocation = await _leaveAllocationRepository.Get(request.LeaveAllocationDto.Id);
            _mapper.Map(request, leaveAllocation);
            await _leaveAllocationRepository.Update(leaveAllocation);
            return Unit.Value;
        }

        private async Task ValidateRequest(UpdateLeaveAllocationCommand request)
        {
            var validator = new UpdateLeaveAllocationDtoValidator(_leaveAllocationRepository);
            var validationResult = await validator.ValidateAsync(request.LeaveAllocationDto);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }
        }
    }
}
