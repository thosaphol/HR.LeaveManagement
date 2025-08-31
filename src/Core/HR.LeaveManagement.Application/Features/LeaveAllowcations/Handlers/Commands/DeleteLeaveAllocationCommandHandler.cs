using System;

namespace HR.LeaveManagement.Application.Features.LeaveAllowcations.Handlers.Commands
{
    using MediatR;
    using System.Threading;
    using System.Threading.Tasks;
    using HR.LeaveManagement.Application.Features.LeaveAllowcations.Requests.Commands;

    using AutoMapper;
    using HR.LeaveManagement.Application.Exceptions;
    using HR.LeaveManagement.Application.Contracts.Persistance;

    public class DeleteLeaveAllocationCommandHandler : IRequestHandler<DeleteLeaveAllocationCommand>
    {
        private readonly ILeaveAllocationRepository _leaveAllocationRepository;
        private readonly IMapper _mapper;

        public DeleteLeaveAllocationCommandHandler(ILeaveAllocationRepository leaveAllocationRepository, IMapper mapper)
        {
            _mapper = mapper;
            _leaveAllocationRepository = leaveAllocationRepository;
        }

        public async Task<Unit> Handle(DeleteLeaveAllocationCommand request, CancellationToken cancellationToken)
        {
            var leaveAllocation = await _leaveAllocationRepository.Get(request.Id);
            ValidateLeaveAllocation(leaveAllocation, request.Id);

            await _leaveAllocationRepository.Delete(leaveAllocation);
            return Unit.Value;
        }
        
        private void ValidateLeaveAllocation(Domain.LeaveAllocation leaveType,int id)
        {
            if (leaveType == null) {
                throw new NotFoundException(nameof(Domain.LeaveAllocation), id);
            }
        }
    }
}
