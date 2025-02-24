using Core.Options;

namespace API.Extensions
{
    public static class AppOptionExtensions
    {
        public static IServiceCollection AddOptionsServices(this IServiceCollection services, IConfiguration configuration)
        {
            //services.Configure<JwtOptions>(configuration.GetSection("Jwt"));
            //UNDONE: Заменить на валидацию с FluentValidation
            services.AddOptions<JwtOptions>()
                .Bind(configuration.GetSection("Jwt"))
                .Validate(options =>
                    !string.IsNullOrEmpty(options.SecretKey) &&
                    !string.IsNullOrEmpty(options.Issuer) &&
                    !string.IsNullOrEmpty(options.Audience), "Incorrect JWT settings");

            return services;
        }
    }
}