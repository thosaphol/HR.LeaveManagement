using System;
using HR.LeaveManagement.Domain.Common;



namespace HR.LeaveManagement.Domain
{
    public class LeaveRequest :BaseDomainEntity
    {
        public int LeaveTypeId { get; set; }
        public LeaveType LeaveType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime RequestedDate { get; set; }
        public string RequestComments { get; set; }
        public DateTime ActionedDate { get; set; }
        public bool? Approved { get; set; }
        public bool Cancelled { get; set; }

        // Additional properties or methods can be added here as needed
    }
}