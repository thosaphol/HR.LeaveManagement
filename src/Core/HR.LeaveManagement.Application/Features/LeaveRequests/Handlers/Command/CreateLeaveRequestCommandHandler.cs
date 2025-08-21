using System;

namespace HR.LeaveManagement.Application.Features.LeaveRequests.Handlers.Command
{
    using MediatR;
    using AutoMapper;
    using HR.LeaveManagement.Application.Features.LeaveRequests.Requests.Commands;
    using HR.LeaveManagement.Application.DTOs.LeaveRequest;
    using HR.LeaveManagement.Domain;
    using HR.LeaveManagement.Application.Persistance.Contracts;
    using System.Threading.Tasks;
    using System.Threading;

    public class CreateLeaveRequestCommandHandler : IRequestHandler<CreateLeaveRequestCommand, int>
    {
        private readonly ILeaveRequestRepository _leaveRequestRepository;
        private readonly IMapper _mapper;

        public CreateLeaveRequestCommandHandler(ILeaveRequestRepository leaveRequestRepository, IMapper mapper)
        {
            _leaveRequestRepository = leaveRequestRepository;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateLeaveRequestCommand request, CancellationToken cancellationToken)
        {
            var leaveRequest = _mapper.Map<LeaveRequest>(request.LeaveRequestDto);
            leaveRequest = await _leaveRequestRepository.Add(leaveRequest);
            return leaveRequest.Id;
        }
    }
}
