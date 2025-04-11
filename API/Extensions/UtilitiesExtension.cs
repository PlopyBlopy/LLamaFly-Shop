using API.Utilities;
using Core.Interfaces;
using Storage.Utilities;

namespace API.Extensions
{
    public static class UtilitiesExtension
    {
        public static IServiceCollection AddAppUtilitiesServices(this IServiceCollection services)
        {
            services.AddScoped<IFileConverter, FileConverter>();
            services.AddScoped<ImageFileReader>();

            return services;
        }
    }
}