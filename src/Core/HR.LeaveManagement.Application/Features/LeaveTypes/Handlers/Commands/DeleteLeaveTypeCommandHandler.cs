using System;

namespace HR.LeaveManagement.Application.Features.LeaveTypes.Handlers.Commands
{
    using AutoMapper;
    using HR.LeaveManagement.Application.Contracts.Persistance;
    using HR.LeaveManagement.Application.Exceptions;
    using HR.LeaveManagement.Application.Features.LeaveTypes.Requests.Commande;
    using HR.LeaveManagement.Domain;
    using MediatR;
    using System.Threading;
    using System.Threading.Tasks;

    public class DeleteLeaveTypeCommandHandler : IRequestHandler<DeleteLeaveTypeCommand, Unit>
    {
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly IMapper _mapper;

        public DeleteLeaveTypeCommandHandler(ILeaveTypeRepository leaveTypeRepository, IMapper mapper)
        {
            _leaveTypeRepository = leaveTypeRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteLeaveTypeCommand request, CancellationToken cancellationToken)
        {
            var leaveType = await _leaveTypeRepository.Get(request.Id);
            ValidateLeaveType(leaveType, request.Id);

            await _leaveTypeRepository.Delete(leaveType);
            return Unit.Value;
        }

        private void ValidateLeaveType(LeaveType leaveType,int id)
        {
            if (leaveType == null) {
                throw new NotFoundException(nameof(LeaveType), id);
            }
        }
    }
}
