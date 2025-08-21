using System;
using HR.LeaveManagement.Application.DTOs;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveTypes.Requests.Queries
{
    public class GetLeaveAllowcationDetailRequest : IRequest<LeaveAllocationDto>
    {
        public int Id { get; set; }

    }
}
