using System;

namespace HR.LeaveManagement.Application.Features.LeaveTypes.Handlers.Queries
{
    using MediatR;
    using AutoMapper;
    using HR.LeaveManagement.Application.Persistance.Contracts;
    using System.Threading.Tasks;
    using HR.LeaveManagement.Application.DTOs;
    using System.Threading;
    using HR.LeaveManagement.Application.Features.LeaveTypes.Requests.Queries;

    public class GetLeaveAllowcationDetailRequestHandler : IRequestHandler<GetLeaveAllowcationDetailRequest, LeaveAllocationDto>
    {
        private readonly ILeaveAllocationRepository _leaveAllocationRepository;
        private readonly IMapper _mapper;

        public GetLeaveAllowcationDetailRequestHandler(ILeaveAllocationRepository leaveAllocationRepository, IMapper mapper)
        {
            _leaveAllocationRepository = leaveAllocationRepository;
            _mapper = mapper;
        }

        public async Task<LeaveAllocationDto> Handle(GetLeaveAllowcationDetailRequest request, CancellationToken cancellationToken)
        {
            var leaveAllowcation = await _leaveAllocationRepository.GetLeaveAllocationWithDetails(request.Id);
            return _mapper.Map<LeaveAllocationDto>(leaveAllowcation);
        }
    }
}
