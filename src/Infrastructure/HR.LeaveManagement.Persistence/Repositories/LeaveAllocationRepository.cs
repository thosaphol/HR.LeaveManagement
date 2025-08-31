using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HR.LeaveManagement.Application.Contracts.Persistance;
using HR.LeaveManagement.Domain;
using Microsoft.EntityFrameworkCore;

namespace HR.LeaveManagement.Persistence.Repositories
{
    public class LeaveAllocationRepository : GenericRepository<LeaveAllocation>, ILeaveAllocationRepository
    {
        private readonly LeaveManagementDbContext _dbContext;
        public LeaveAllocationRepository(LeaveManagementDbContext _bContext) : base(_bContext)
        {
            _dbContext = _bContext;
        }

        public async Task<LeaveAllocation> GetLeaveAllocationWithDetails(int id)
        {
            var leaveALlocation = await _dbContext.LeaveAllocations
            .Include(q => q.LeaveType).FirstOrDefaultAsync(q => q.Id == id);
            return leaveALlocation;
        }

        public async Task<List<LeaveAllocation>> GetLeaveAllocationWithDetails()
        {
            var leaveALlocations = await _dbContext.LeaveAllocations
            .Include(q => q.LeaveType).ToListAsync();
            return leaveALlocations;
        }
    }
}
