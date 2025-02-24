using Application.Utilities;

namespace API.Extensions
{
    public static class AppUtilitiesExtension
    {
        public static IServiceCollection AddAppUtilitiesServices(this IServiceCollection services)
        {
            services.AddScoped<BuildCategoryHierarchy>();

            return services;
        }
    }
}