using System;
using HR.LeaveManagement.Application.Contracts.Persistance;
using HR.LeaveManagement.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HR.LeaveManagement.Persistence
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection ConfigurePersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            // services.AddDbContext<LeaveManagementDbContext>(options =>
            //     options.UseSqlServer(configuration.GetConnectionString("LeaveManagementConnectionString")));
                services.AddDbContext<LeaveManagementDbContext>(options =>
                options.UseSqlite(configuration.GetConnectionString("LeaveManagementConnectionString")));

            services.AddScoped<ILeaveTypeRepository, LeaveTypeRepository>();
            services.AddScoped<ILeaveAllocationRepository, LeaveAllocationRepository>();
            services.AddScoped<ILeaveRequestRepository, LeaveRequestRepository>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            return services;
        }
    }
}
