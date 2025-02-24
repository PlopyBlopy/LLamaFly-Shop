using API.Mappings.Profiles;
using Application.Mappings.Profiles;

namespace API.Extensions
{
    public static class MapperExtension
    {
        public static IServiceCollection AddMappingServices(this IServiceCollection services)
        {
            services.AddAutoMapper(
                typeof(UserApiProfile),
                typeof(AdminApiProfile),
                typeof(SellerApiProfile),
                typeof(CustomerApiProfile),

                typeof(UserAppProfile),
                typeof(AdminAppProfile),
                typeof(SellerAppProfile),
                typeof(CustomerAppProfile));
            return services;
        }
    }
}