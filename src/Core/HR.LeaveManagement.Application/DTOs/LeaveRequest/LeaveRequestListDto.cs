using System;
using HR.LeaveManagement.Application.DTOs.Common;

namespace HR.LeaveManagement.Application.DTOs.LeaveRequest
{
    public class LeaveRequestListDto :BaseDto
    {
        public LeaveTypeDto LeaveType { get; set; }
        public DateTime RequestedDate { get; set; }
        public bool? Approved { get; set; }
    }
}
