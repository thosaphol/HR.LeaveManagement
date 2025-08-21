using System;

namespace HR.LeaveManagement.Application.Features.LeaveAllowcations.Handlers.Commands
{
    using MediatR;
    using System.Threading;
    using System.Threading.Tasks;
    using HR.LeaveManagement.Application.Features.LeaveAllowcations.Requests.Commands;
    using HR.LeaveManagement.Application.Persistance.Contracts;
    using AutoMapper;

    public class DeleteLeaveAllocationCommandHandler : IRequestHandler<DeleteLeaveAllocationCommand>
    {
        private readonly ILeaveAllocationRepository _leaveAllocationRepository;
        private readonly IMapper _mapper;

        public DeleteLeaveAllocationCommandHandler(ILeaveAllocationRepository leaveAllocationRepository,IMapper mapper)
        {
            _mapper = mapper;
            _leaveAllocationRepository = leaveAllocationRepository;
        }

        public async Task<Unit> Handle(DeleteLeaveAllocationCommand request, CancellationToken cancellationToken)
        {
            var leaveAllocation = await _leaveAllocationRepository.Get(request.Id);
            if (leaveAllocation == null)
            {
                return Unit.Value;
            }

            await _leaveAllocationRepository.Delete(leaveAllocation);
            return Unit.Value;
        }
    }
}
