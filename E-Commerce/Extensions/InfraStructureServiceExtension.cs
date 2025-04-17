using Domain.Contracts;
using Presistance.Data;
using Presistance.Repositories;
using Presistance;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;
using StackExchange.Redis;
using Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace E_Commerce.Extensions
{
    public static class InfraStructureServiceExtension
    {
        public static IServiceCollection AddInfraStructureService(this IServiceCollection Services,IConfiguration configuration)
        {
            Services.AddDbContext<StoreContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });
            Services.AddDbContext<StoreIdentityContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("IdentityConnection"));
            });
            Services.AddScoped<IDbInitializer, DbInitializer>();
            Services.AddScoped<IUnitOfWork, UnitOfWork>();
            Services.AddSingleton<IConnectionMultiplexer>(
                _=>ConnectionMultiplexer.Connect(configuration.GetConnectionString("Redis")));
            Services.AddScoped<IBasketRepository, BasketRepository>();
            Services.ConfigureIdentityService();
            return Services;
        }
        public static IServiceCollection ConfigureIdentityService(this IServiceCollection Services)
        {
            Services.AddIdentity<User, IdentityRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 8;
            }).AddEntityFrameworkStores<StoreIdentityContext>();
            return Services;
        }
        
    }
}
