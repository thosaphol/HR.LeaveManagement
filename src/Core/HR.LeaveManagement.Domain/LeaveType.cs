using System;
using HR.LeaveManagement.Domain.Common;

namespace HR.LeaveManagement.Domain
{
    public class LeaveType: BaseDomainEntity
    {
        public string Name { get; set; }
        public int DefaultDays { get; set; }
        public bool IsActive { get; set; }

        // Additional properties or methods can be added here as needed
    }
}
