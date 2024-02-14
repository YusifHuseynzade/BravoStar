using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ApplicationUserDetails
{
    public static class ConfigureService
    {
        public static IServiceCollection AddApplicationUserServices(this IServiceCollection services)
        {
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());
            });
            return services;
        }
    }
}
