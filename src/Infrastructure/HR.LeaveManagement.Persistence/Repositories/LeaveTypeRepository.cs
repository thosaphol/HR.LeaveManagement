using System;
using HR.LeaveManagement.Application.Persistance.Contracts;
using HR.LeaveManagement.Domain;

namespace HR.LeaveManagement.Persistence.Repositories
{
    public class LeaveTypeRepository : GenericRepository<LeaveType>, ILeaveTypeRepository
    {
        public LeaveTypeRepository(LeaveManagementDbContext _bContext) : base(_bContext)
        {
        }
    }
}
