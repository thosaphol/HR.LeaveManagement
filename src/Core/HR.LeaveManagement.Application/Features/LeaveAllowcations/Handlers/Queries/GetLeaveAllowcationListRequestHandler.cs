using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using HR.LeaveManagement.Application.DTOs;
using HR.LeaveManagement.Application.Features.LeaveTypes.Requests.Queries;
using HR.LeaveManagement.Application.Persistance.Contracts;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveTypes.Handlers.Queries
{
    public class GetLeaveAllowcationListRequestHandler : IRequestHandler<GetLeaveAllowcationListRequest, List<LeaveAllocationDto>>
    {
        private readonly IMapper _mapper;
        private readonly ILeaveAllocationRepository _leaveAllowcationRepository;

        public GetLeaveAllowcationListRequestHandler(IMapper mapper, ILeaveAllocationRepository leaveAllowcationRepository)
        {
            _mapper = mapper;
            _leaveAllowcationRepository = leaveAllowcationRepository;
        }

        public async Task<List<LeaveAllocationDto>> Handle(GetLeaveAllowcationListRequest request, CancellationToken cancellationToken)
        {
            var leaveAllowcations = await _leaveAllowcationRepository.GetAll();
            return _mapper.Map<List<LeaveAllocationDto>>(leaveAllowcations);
        }
    }
}
