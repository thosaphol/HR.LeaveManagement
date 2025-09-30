using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR.LeaveManagement.Identity.Configurations;

public class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
{
    public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
    {
        builder.HasData(
            new IdentityUserRole<string>
            {
                RoleId = "8d04dce2-969a-435d-bba4-df3f325983dc",
                UserId = "8d04dce2-969a-435d-bba4-df3f325983dc"
            },
            new IdentityUserRole<string>
            {
                RoleId = "9d04dce2-969a-435d-bba4-df3f325983dd",
                UserId = "9d04dce2-969a-435d-bba4-df3f325983dd"
            }
        );  
    }
}
