using System;
using System.Reflection;
using HR.LeaveManagement.Application.Profiles;
using Microsoft.Extensions.DependencyInjection;

namespace HR.LeaveManagement.Application
{
    public static class ApplicationServicesRegistration 
    {
        public static void ConfigureApplicationServices(this IServiceCollection services)
        {
            // Register application services here
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            // Other service registrations can go here
        }
    }
}
