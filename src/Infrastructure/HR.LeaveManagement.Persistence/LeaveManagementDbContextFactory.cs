using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace HR.LeaveManagement.Persistence
{
    public class LeaveManagementDbContextFactory : IDesignTimeDbContextFactory<LeaveManagementDbContext>
    {
    
        public LeaveManagementDbContext CreateDbContext(string[] args)
        {
            IConfiguration configurationRoot =  new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();
            
            var optionsBuilder = new DbContextOptionsBuilder<LeaveManagementDbContext>();
            optionsBuilder.UseSqlite(configurationRoot.GetConnectionString("LeaveManagementConnectionString"));

            return new LeaveManagementDbContext(optionsBuilder.Options);
        }
    }
}
