using Application.Services;
using Core.Interfaces;

namespace API.Extensions
{
    public static class AppServiceExtension
    {
        public static IServiceCollection AddAppServices(this IServiceCollection services)
        {
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICategoryService, CategoryService>();

            return services;
        }
    }
}