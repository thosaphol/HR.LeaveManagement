using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace HR.LeaveManagement.Identity;

public class LeaveManagementIdentityDbContextFactory : IDesignTimeDbContextFactory<LeaveManagementIdentityDbContext>
{
    public LeaveManagementIdentityDbContext CreateDbContext(string[] args)
    {
        IConfiguration configurationRoot =  new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();
            
            var optionsBuilder = new DbContextOptionsBuilder<LeaveManagementIdentityDbContext>();
            optionsBuilder.UseSqlite(configurationRoot.GetConnectionString("LeaveManagementIdentityConnectionString"),
                b => b.MigrationsAssembly(typeof(LeaveManagementIdentityDbContext).Assembly.FullName));

            return new LeaveManagementIdentityDbContext(optionsBuilder.Options);
    }
}
