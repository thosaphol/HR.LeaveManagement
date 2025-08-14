using System;
using HR.LeaveManagement.Domain.Common;



namespace HR.LeaveManagement.Domain
{
    public class LeaveAllocation : BaseDomainEntity
    {
        public int LeaveTypeId { get; set; }
        public LeaveType LeaveType { get; set; }
        public int NumberOfDays { get; set; }
        public int Preiod { get; set; }

        // Additional properties or methods can be added here as needed
    }
}
