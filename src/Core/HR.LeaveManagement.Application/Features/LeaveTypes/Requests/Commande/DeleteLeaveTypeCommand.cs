using System;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveTypes.Requests.Commande
{
    public class DeleteLeaveTypeCommand : IRequest
    {
        public int Id { get; set; }
    }
}
