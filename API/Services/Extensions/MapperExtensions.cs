using API.Mappings.Profiles;
using Application.Mappings.Profiles;
using DataBase.Mappings.Profiles;

namespace API.Services.Extensions
{
    public static class MapperExtensions
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