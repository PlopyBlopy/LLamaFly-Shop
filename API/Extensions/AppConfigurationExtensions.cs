using API.Properties;
using Core.Interfaces;

namespace API.Extensions
{
    public static class AppConfigurationExtensions
    {
        public static IServiceCollection AddAppConfigurations(this IServiceCollection services)
        {
            services.AddSingleton<IStoragePathProvider, WebStoragePathProvider>();

            return services;
        }
    }
}