using System;
using HR.LeaveManagement.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR.LeaveManagement.Persistence.Configurations.Entities;

public class LeaveTypeConfiguration : IEntityTypeConfiguration<LeaveType>
{
    public void Configure(EntityTypeBuilder<LeaveType> builder)
    {
       builder.HasData(
           new LeaveType
           {
               Id = 1,
               Name = "Vacation",
               DefaultDays = 10,
               CreatedBy = "System",
               LastModifiedBy = "System",
           },
           new LeaveType
           {
               Id = 2,
               Name = "Sick",
               DefaultDays = 12,
               CreatedBy = "System",
               LastModifiedBy = "System",
           }
       );
    }
}
