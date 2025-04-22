using Domain.Contracts;
using E_Commerce.Middlewares;

namespace E_Commerce.Extensions
{
    public static class WebApplicationExtensions
    {
        public static async Task<WebApplication> SeedDbAsync(this WebApplication app)
        {
            //Create Object From Type That Implements IDbinitializer
            using var Scope = app.Services.CreateScope();
            var dbinitializer = Scope.ServiceProvider.GetRequiredService<IDbInitializer>();
            await dbinitializer.InitializeAsync();
            await dbinitializer.InitializeIdentityAsync();
            return app;
        }
        public static WebApplication UseCustomMiddleWareExceptions(this WebApplication app)
        {
            app.UseMiddleware<GlobalExceptionHandlingMiddleware>();
            return app;
        }
    }
}
