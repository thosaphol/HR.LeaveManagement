using System;
using HR.LeaveManagement.Application.DTOs.Common;

namespace HR.LeaveManagement.Application.DTOs.LeaveRequest
{
    public class ChangeLeaveRequestApproveDto : BaseDto
    {
        public bool? Approved { get; set; }
    }
}
