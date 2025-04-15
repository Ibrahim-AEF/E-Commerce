using Services.Abstractions;
using Services;
using E_Commerce.Factories;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Extensions
{
    public static class PresentationServiceExtensions
    {
        public static IServiceCollection AddPresentationServices(this IServiceCollection Services)
        {
            Services.AddControllers().AddApplicationPart(typeof(Presentation.AssemblyReference).Assembly);
            Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = ApiResponseFactory.CustomValidationErrors;
            });
            return Services;
        }
    }
}
