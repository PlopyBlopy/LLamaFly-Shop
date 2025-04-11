using Application.Utilities;
using Core.Interfaces;

namespace API.Extensions
{
    public static class UtilitiesExtension
    {
        public static IServiceCollection AddAppUtilitiesServices(this IServiceCollection services)
        {
            services.AddScoped<IPasswordHasher, PasswordHasher>();
            return services;
        }
    }
}