using System;
using System.Collections.Generic;
using HR.LeaveManagement.Application.DTOs;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Requests.Queries
{
    public class GetLeaveTypeListRequest :IRequest<List<LeaveTypeDto>>
    {

    }
}
