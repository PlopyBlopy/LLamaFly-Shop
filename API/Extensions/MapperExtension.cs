using Application.Mappings.Profiles;

namespace API.Extensions
{
    public static class MapperExtension
    {
        public static IServiceCollection AddMappingServices(this IServiceCollection services)
        {
            services.AddAutoMapper(
                typeof(ProfileAppProfile),
                typeof(UserAppProfile),
                typeof(AdminAppProfile),
                typeof(SellerAppProfile),
                typeof(CustomerAppProfile));

            return services;
        }
    }
}