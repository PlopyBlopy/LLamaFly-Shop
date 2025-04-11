namespace API.Extensions
{
    public static class AddAuthorizationSettingsExtension
    {
        public static IServiceCollection AddAuthorizationSettingsServices(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy("admin", policy =>
                    policy.RequireRole("admin"));
                options.AddPolicy("seller", policy =>
                    policy.RequireRole("admin", "seller"));
            });

            return services;
        }
    }
}