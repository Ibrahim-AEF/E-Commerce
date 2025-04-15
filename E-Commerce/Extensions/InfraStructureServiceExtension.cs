using Domain.Contracts;
using Presistance.Data;
using Presistance.Repositories;
using Presistance;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;
using StackExchange.Redis;

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
            Services.AddScoped<IDbInitializer, DbInitializer>();
            Services.AddScoped<IUnitOfWork, UnitOfWork>();
            Services.AddSingleton<IConnectionMultiplexer>(
                _=>ConnectionMultiplexer.Connect(configuration.GetConnectionString("Redis")));
            Services.AddScoped<IBasketRepository, BasketRepository>();
            return Services;
        }
        
    }
}
