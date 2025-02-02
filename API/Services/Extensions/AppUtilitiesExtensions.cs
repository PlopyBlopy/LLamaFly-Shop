using DataBase.Utilities;

namespace API.Services.Extensions
{
    public static class AppUtilitiesExtensions
    {
        public static IServiceCollection AddAppUtilities(this IServiceCollection services)
        {
            services.AddScoped<CategoryEntityToDtoRecursionConverter>();

            return services;
        }
    }
}