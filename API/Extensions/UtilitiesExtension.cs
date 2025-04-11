using API.Utillities;

namespace API.Extensions
{
    public static class UtilitiesExtension
    {
        public static IServiceCollection AddAppUtilitiesServices(this IServiceCollection services)
        {
            services.AddScoped<IHttpContextTokenReader, HttpContextTokenReader>();

            return services;
        }
    }
}