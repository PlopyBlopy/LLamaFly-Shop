using Application.Services;
using Core.Interfaces;
using Storage.Services;

namespace API.Extensions
{
    public static class AppServiceExtensions
    {
        public static IServiceCollection AddAppServices(this IServiceCollection services)
        {
            services.AddScoped<IImageService, ImageService>();
            services.AddScoped<IStorageService, StorageService>();

            return services;
        }
    }
}