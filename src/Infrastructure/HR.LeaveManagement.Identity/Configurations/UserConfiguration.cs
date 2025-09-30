using System;
using HR.LeaveManagement.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR.LeaveManagement.Identity.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        var hash = new PasswordHasher<ApplicationUser>();

        builder.HasData(
            new ApplicationUser
            {
                Id = "8d04dce2-969a-435d-bba4-df3f325983dc",
                UserName = "admin@localhost.com",
                NormalizedUserName = "ADMIN@LOCALHOST.COM",
                Email = "admin@localhost.com",
                PasswordHash = hash.HashPassword(null, "P@ssword1"),
                FirstName = "System",
                LastName = "Admin",
                NormalizedEmail = "ADMIN@LOCALHOST.COM",
                EmailConfirmed = true,
            },
            new ApplicationUser
            {
                Id = "9d04dce2-969a-435d-bba4-df3f325983dd",
                UserName = "user@localhost.com",
                NormalizedUserName = "USER@LOCALHOST.COM",
                Email = "user@localhost.com",
                PasswordHash = hash.HashPassword(null, "P@ssword1"),
                FirstName = "System",
                LastName = "User",
                NormalizedEmail = "USER@LOCALHOST.COM",
                EmailConfirmed = true,
            });
    }
}
