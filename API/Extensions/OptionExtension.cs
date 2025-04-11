using Core.Options;

namespace API.Extensions
{
    public static class OptionExtension
    {
        public static IServiceCollection AddConfigServices(this IServiceCollection services, IConfiguration configuration)
        {
            var profileServiceEndpoints = configuration.GetRequiredSection("HttpClients:ProfileServiceUrl:ApiEndpoints");

            services.Configure<ProfileServiceEndpointsOptions>(profileServiceEndpoints);

            return services;
        }
    }
}

//services.AddOptions<UrlsConfig>()
//    .Bind(configuration.GetRequiredSection("Urls"))
//    .ValidateDataAnnotations()
//    .ValidateOnStart();