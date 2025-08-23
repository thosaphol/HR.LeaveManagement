using System;

namespace HR.LeaveManagement.Application.Features.LeaveAllowcations.Requests.Commands
{
    using MediatR;
    using HR.LeaveManagement.Application.DTOs.LeaveAllocation;
    using HR.LeaveManagement.Application.DTOs;
    using HR.LeaveManagement.Application.Responses;

    public class CreateLeaveAllocationCommand : IRequest<BaseCommandResponse>
    {
        public CreateLeaveAllocationDto LeaveAllocationDto { get; set; }
    }
}
