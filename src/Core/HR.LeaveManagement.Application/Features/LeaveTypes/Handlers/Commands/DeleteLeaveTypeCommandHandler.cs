using System;

namespace HR.LeaveManagement.Application.Features.LeaveTypes.Handlers.Commands
{
    using AutoMapper;
    using HR.LeaveManagement.Application.Features.LeaveTypes.Requests.Commande;
    using HR.LeaveManagement.Application.Persistance.Contracts;
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
            if (leaveType == null)
            {
                // Optionally throw NotFoundException or handle as needed
                return Unit.Value;
            }

            await _leaveTypeRepository.Delete(leaveType);
            return Unit.Value;
        }
    }
}
