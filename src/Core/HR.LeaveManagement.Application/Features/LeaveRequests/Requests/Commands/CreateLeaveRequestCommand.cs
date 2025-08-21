using System;

namespace HR.LeaveManagement.Application.Features.LeaveRequests.Requests.Commands
{
    using HR.LeaveManagement.Application.DTOs.LeaveRequest;
    using MediatR;

    public class CreateLeaveRequestCommand : IRequest<int>
    {
        public CreateLeaveRequestDto LeaveRequestDto { get; set; }
    }
}
