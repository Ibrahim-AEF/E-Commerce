using Services.Abstractions;
using Services;
using System.Runtime.CompilerServices;
using Shared.Security;

namespace E_Commerce.Extensions
{
    public static class CoreServicesExtension
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection Services,IConfiguration configuration)
        {
            Services.AddScoped<IServiceManager, ServiceManager>();
            Services.AddAutoMapper(typeof(Services.AssemblyReference).Assembly);
            Services.Configure<Jwtoptions>(configuration.GetSection("JwtOptions"));
            return Services;
        }
    }
}
