using System;

namespace HR.LeaveManagement.Application.Features.LeaveAllowcations.Handlers.Commands
{
    using MediatR;
    using AutoMapper;
    using HR.LeaveManagement.Application.Features.LeaveAllowcations.Requests.Commands;
    using System.Threading;
    using System.Threading.Tasks;
    using HR.LeaveManagement.Application.Persistance.Contracts;

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
            var leaveAllocation = await _leaveAllocationRepository.Get(request.LeaveAllocationDto.Id);
            _mapper.Map(request, leaveAllocation);
            await _leaveAllocationRepository.Update(leaveAllocation);
            return Unit.Value;
        }
    }
}
