using System;

namespace HR.LeaveManagement.Application.Features.LeaveRequests.Requests.Commands
{
    using HR.LeaveManagement.Application.DTOs.LeaveRequest;
    using HR.LeaveManagement.Application.Responses;
    using MediatR;

    public class CreateLeaveRequestCommand : IRequest<BaseCommandResponse>
    {
        public CreateLeaveRequestDto LeaveRequestDto { get; set; }
    }
}
