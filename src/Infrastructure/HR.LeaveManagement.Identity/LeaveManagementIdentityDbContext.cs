using System;
using HR.LeaveManagement.Identity.Configurations;
using HR.LeaveManagement.Identity.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HR.LeaveManagement.Identity;

public class LeaveManagementIdentityDbContext : IdentityDbContext<ApplicationUser>
{
    public LeaveManagementIdentityDbContext(DbContextOptions<LeaveManagementIdentityDbContext> options) : base(options)
    {

    }

    override protected void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfiguration(new RoleConfiguration());
        builder.ApplyConfiguration(new UserRoleConfiguration());
        builder.ApplyConfiguration(new UserConfiguration());
        
    }

}
