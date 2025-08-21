using System;
using HR.LeaveManagement.Application.DTOs.Common;
using HR.LeaveManagement.Domain;

namespace HR.LeaveManagement.Application.DTOs
{
    public class LeaveAllocationDto : BaseDto
    {

        public int LeaveTypeId { get; set; }
        public LeaveTypeDto LeaveType { get; set; }
        public int NumberOfDays { get; set; }
        public int Preiod { get; set; }
    }

}
