namespace API.Extensions
{
    public static class CorsExtension
    {
        public static IServiceCollection AddCorsService(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddCors(options =>
            {
                var frontendUrl = configuration.GetRequiredSection("Urls:FrontendServiceUrl");

                options.AddPolicy("AllowFrontend", builder =>
                {
                    builder.WithOrigins(frontendUrl.Value!)
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                });
            });
        }
    }
}