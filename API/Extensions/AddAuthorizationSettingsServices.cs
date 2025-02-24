namespace API.Extensions
{
    public static class AddAuthorizationSettingsExtension
    {
        public static IServiceCollection AddAuthorizationSettingsServices(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy("Admin", policy =>
                    policy.RequireRole("Admin"));
                options.AddPolicy("Seller", policy =>
                    policy.RequireRole("Admin", "Seller"));
            });

            return services;
        }
    }
}