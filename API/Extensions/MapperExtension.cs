using API.Mappings.Profiles;
using Application.Mappings.Profiles;

namespace API.Extensions
{
    public static class MapperExtension
    {
        public static IServiceCollection AddMappingServices(this IServiceCollection services)
        {
            services.AddAutoMapper(
                typeof(ProductApiProfile),
                typeof(ProductApplicationProfile),
                typeof(CategoryApiProfile),
                typeof(CategoryAppicationProfile));
            return services;
        }
    }
}