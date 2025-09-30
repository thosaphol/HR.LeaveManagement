using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR.LeaveManagement.Identity.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
{
    public void Configure(EntityTypeBuilder<IdentityRole> builder)
    {
        builder.HasData(
            new IdentityRole
            {
                Id = "8d04dce2-969a-435d-bba4-df3f325983dc",
                Name = "Administrator",
                NormalizedName = "ADMINISTRATOR",
                ConcurrencyStamp = "1"
            },
            new IdentityRole
            {
                Id = "9d04dce2-969a-435d-bba4-df3f325983dd",
                Name = "Employee",
                NormalizedName = "EMPLOYEE",
                ConcurrencyStamp = "2"
            }
        );
    }
}
