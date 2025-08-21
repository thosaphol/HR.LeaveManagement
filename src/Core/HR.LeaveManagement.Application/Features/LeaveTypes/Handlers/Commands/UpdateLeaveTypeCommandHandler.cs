using System;

namespace HR.LeaveManagement.Application.Features.LeaveTypes.Handlers.Commands
{
    using MediatR;
    using AutoMapper;
    using HR.LeaveManagement.Domain;
    using HR.LeaveManagement.Application.Persistance.Contracts;
    using System.Threading;
    using HR.LeaveManagement.Application.Features.LeaveTypes.Requests.Commande;
    using System.Threading.Tasks;

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
            var leaveType = await _leaveTypeRepository.Get(request.LeaveTypeDto.Id);
            _mapper.Map(request, leaveType);
            await _leaveTypeRepository.Update(leaveType);
            return Unit.Value;
        }
    }
}
