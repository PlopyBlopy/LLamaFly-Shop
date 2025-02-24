using Core.Interfaces;
using Infrastructure.DataBases.Dapper.Repositories;

namespace API.Extensions
{
    public static class AppRepositoryExtension
    {
        public static IServiceCollection AddRepositoryServices(this IServiceCollection service)
        {
            service.AddScoped<IUserRepository, UserRepository>();

            return service;
        }
    }
}