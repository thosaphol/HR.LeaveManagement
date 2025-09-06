using System;
using HR.LeaveManagement.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR.LeaveManagement.Persistence.Configurations.Entities;

public class LeaveAllocationConfiguration :IEntityTypeConfiguration<LeaveAllocation>
{
    public void Configure(EntityTypeBuilder<Domain.LeaveAllocation> builder)
    {
        // throw new NotImplementedException();
    }


}
