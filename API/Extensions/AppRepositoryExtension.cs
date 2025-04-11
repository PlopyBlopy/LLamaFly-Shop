using Core.Interfaces;
using Infrastructure.DataBases.Dapper.Repositories;

namespace API.Extensions
{
    public static class AppRepositoryExtension
    {
        public static IServiceCollection AddRepositoryServices(this IServiceCollection service)
        {
            service.AddScoped<IUserRepository, UserRepository>();
            service.AddScoped<IProfileRepository, ProfileRespository>();
            service.AddScoped<IAdminRepository, AdminRepository>();
            service.AddScoped<ISellerRepository, SellerRepository>();
            service.AddScoped<ICustomerRepository, CustomerRepository>();

            return service;
        }
    }
}