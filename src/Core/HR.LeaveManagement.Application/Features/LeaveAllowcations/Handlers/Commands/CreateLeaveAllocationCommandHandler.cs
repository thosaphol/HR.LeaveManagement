using System;

namespace HR.LeaveManagement.Application.Features.LeaveAllowcations.Handlers.Commands
{
    using MediatR;
    using HR.LeaveManagement.Application.Features.LeaveAllowcations.Requests.Commands;
    using AutoMapper;

    using HR.LeaveManagement.Domain;
    using HR.LeaveManagement.Application.Persistance.Contracts;
    using System.Threading.Tasks;
    using System.Threading;

    public class CreateLeaveAllocationCommandHandler : IRequestHandler<CreateLeaveAllocationCommand, int>
    {
        private readonly ILeaveAllocationRepository _leaveAllocationRepository;
        private readonly IMapper _mapper;

        public CreateLeaveAllocationCommandHandler(ILeaveAllocationRepository leaveAllocationRepository, IMapper mapper)
        {
            _leaveAllocationRepository = leaveAllocationRepository;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateLeaveAllocationCommand request, CancellationToken cancellationToken)
        {
            var leaveAllocation = _mapper.Map<LeaveAllocation>(request.LeaveAllocationDto);
            leaveAllocation = await _leaveAllocationRepository.Add(leaveAllocation);
            return leaveAllocation.Id;
        }
    }
}
