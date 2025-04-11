using API.Mapping.Profiles;

namespace API.Extensions
{
    public static class MapperExtensions
    {
        public static IServiceCollection AddMappingServices(this IServiceCollection services)
        {
            services.AddAutoMapper(
                typeof(ImageApiProfile));
            return services;
        }
    }
}