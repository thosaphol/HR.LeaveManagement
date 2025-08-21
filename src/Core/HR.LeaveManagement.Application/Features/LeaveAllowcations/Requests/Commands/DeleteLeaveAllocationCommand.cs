using System;

namespace HR.LeaveManagement.Application.Features.LeaveAllowcations.Requests.Commands
{
    using MediatR;

    public class DeleteLeaveAllocationCommand : IRequest
    {
        public int Id { get; set; }
    }
}
