namespace API.Extensions
{
    public static class AuthorizationSettingsExtension
    {
        public static IServiceCollection AddAuthorizationSettingsServices(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy("admin", policy =>
                    policy.RequireRole("admin"));
                options.AddPolicy("seller", policy =>
                    policy.RequireRole("admin", "seller"));
                options.AddPolicy("customer", policy =>
                   policy.RequireRole("admin", "customer"));
            });

            return services;
        }
    }
}