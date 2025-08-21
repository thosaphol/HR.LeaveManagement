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
    using HR.LeaveManagement.Application.DTOs.LeaveRequest;

    public class GetLeaveRequestDetailRequestHandler : IRequestHandler<GetLeaveRequestDetailRequest, LeaveRequestDto>
    {
        private readonly ILeaveRequestRepository _leaveRequestRepository;
        private readonly IMapper _mapper;

        public GetLeaveRequestDetailRequestHandler(ILeaveRequestRepository leaveRequestRepository, IMapper mapper)
        {
            _leaveRequestRepository = leaveRequestRepository;
            _mapper = mapper;
        }

        public async Task<LeaveRequestDto> Handle(GetLeaveRequestDetailRequest request, CancellationToken cancellationToken)
        {
            var leaveRequest = await _leaveRequestRepository.GetLeaveRequestWithDetails(request.Id);
            return _mapper.Map<LeaveRequestDto>(leaveRequest);
        }
    }
}
