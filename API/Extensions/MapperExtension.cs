using Application.Mappings.Profiles;

namespace API.Extensions
{
    public static class MapperExtension
    {
        public static IServiceCollection AddMappingServices(this IServiceCollection services)
        {
            services.AddAutoMapper(
                typeof(AdminAppProfile),
                typeof(ProfileAppProfile));

            return services;
        }
    }
}