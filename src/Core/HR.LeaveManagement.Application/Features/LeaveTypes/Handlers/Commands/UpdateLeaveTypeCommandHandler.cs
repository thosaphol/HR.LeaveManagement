using System;

namespace HR.LeaveManagement.Application.Features.LeaveTypes.Handlers.Commands
{
    using MediatR;
    using AutoMapper;
    using HR.LeaveManagement.Domain;
    using System.Threading;
    using HR.LeaveManagement.Application.Features.LeaveTypes.Requests.Commande;
    using System.Threading.Tasks;
    using HR.LeaveManagement.Application.DTOs.LeaveType.Validators;
    using HR.LeaveManagement.Application.Exceptions;
    using HR.LeaveManagement.Application.Contracts.Persistance;

    public class UpdateLeaveTypeCommandHandler : IRequestHandler<UpdateLeaveTypeCommand, Unit>
    {
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly IMapper _mapper;

        public UpdateLeaveTypeCommandHandler(ILeaveTypeRepository leaveTypeRepository, IMapper mapper)
        {
            _leaveTypeRepository = leaveTypeRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateLeaveTypeCommand request, CancellationToken cancellationToken)
        {
            await ValidateRequest(request);
            var leaveType = await _leaveTypeRepository.Get(request.LeaveTypeDto.Id);
            _mapper.Map(request, leaveType);
            await _leaveTypeRepository.Update(leaveType);
            return Unit.Value;
        }

        private async Task ValidateRequest(UpdateLeaveTypeCommand request)
        {
            var validator = new UpdateLeaveTypeDtoValidator();
             var validationResult = await validator.ValidateAsync(request.LeaveTypeDto);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult);
            }
        }
    }
}
