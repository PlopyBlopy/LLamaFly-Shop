using Application.Services;
using Core.Interfaces;

namespace API.Extensions
{
    public static class AppServiceExtension
    {
        public static IServiceCollection AddAppServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IProfileService, ProfileService>();
            services.AddScoped<IAdminService, AdminService>();
            services.AddScoped<ISellerService, SellerService>();
            services.AddScoped<ICustomerService, CustomerService>();

            return services;
        }
    }
}