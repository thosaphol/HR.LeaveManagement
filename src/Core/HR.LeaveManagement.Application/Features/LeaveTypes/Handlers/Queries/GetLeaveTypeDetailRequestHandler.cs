using System;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Handlers.Queries
{
    using MediatR;
    using AutoMapper;
    using HR.LeaveManagement.Application.Persistance.Contracts;
    using System.Threading.Tasks;
    using HR.LeaveManagement.Application.DTOs;
    using System.Threading;
    using HR.LeaveManagement.Application.Features.LeaveRequest.Requests.Queries;

    public class GetLeaveTypeDetailRequestHandler : IRequestHandler<GetLeaveTypeDetailRequest, LeaveTypeDto>
    {
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly IMapper _mapper;

        public GetLeaveTypeDetailRequestHandler(ILeaveTypeRepository leaveTypeRepository, IMapper mapper)
        {
            _leaveTypeRepository = leaveTypeRepository;
            _mapper = mapper;
        }

        public async Task<LeaveTypeDto> Handle(GetLeaveTypeDetailRequest request, CancellationToken cancellationToken)
        {
            var leaveType = await _leaveTypeRepository.GetLeaveTypeWithDetails(request.Id);
            return _mapper.Map<LeaveTypeDto>(leaveType);
        }
    }
}
