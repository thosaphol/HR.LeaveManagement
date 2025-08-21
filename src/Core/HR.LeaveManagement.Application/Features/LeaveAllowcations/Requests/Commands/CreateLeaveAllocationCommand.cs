using System;

namespace HR.LeaveManagement.Application.Features.LeaveAllowcations.Requests.Commands
{
    using MediatR;
    using HR.LeaveManagement.Application.DTOs.LeaveAllocation;
    using HR.LeaveManagement.Application.DTOs;

    public class CreateLeaveAllocationCommand : IRequest<int>
    {
        public LeaveAllocationDto LeaveAllocationDto { get; set; }
    }
}
