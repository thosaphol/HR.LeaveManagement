using System;
using HR.LeaveManagement.Application.DTOs;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveTypes.Requests.Commande
{
    public class CreateLeaveTypeCommand :IRequest<int>
    {
        public LeaveTypeDto LeaveTypeDto { get; set; }
    }
}
