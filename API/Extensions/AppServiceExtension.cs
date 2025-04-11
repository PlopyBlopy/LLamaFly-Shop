using Application.Services;
using Core.Interfaces;
using Infrastructure.API.ProfileService.Endpoints;

namespace API.Extensions
{
    public static class AppServiceExtension
    {
        public static IServiceCollection AddAppServices(this IServiceCollection services)
        {
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IProfileService, ProfileApiService>();

            return services;
        }
    }
}