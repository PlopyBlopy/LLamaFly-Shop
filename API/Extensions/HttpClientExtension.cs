namespace API.Extensions
{
    public static class HttpClientExtension
    {
        public static IServiceCollection AddHttpClientServices(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddHttpClient("ProfileServiceApi", client =>
            //{
            //    var clientUrl = configuration.GetRequiredSection("HttpClients:ProfileServiceUrl:BaseUrl");
            //    client.BaseAddress = new Uri(clientUrl.Value);
            //});

            services.AddHttpClient("ProfileServiceApi", client =>
            {
                var clientUrl = configuration["HttpClients:ProfileServiceUrl:BaseUrl"];
                client.BaseAddress = new Uri(clientUrl);
            });

            return services;
        }
    }
}