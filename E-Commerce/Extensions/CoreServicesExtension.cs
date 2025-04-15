using Services.Abstractions;
using Services;
using System.Runtime.CompilerServices;

namespace E_Commerce.Extensions
{
    public static class CoreServicesExtension
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection Services)
        {
            Services.AddScoped<IServiceManager, ServiceManager>();
            Services.AddAutoMapper(typeof(Services.AssemblyReference).Assembly);
            return Services;
        }
    }
}
