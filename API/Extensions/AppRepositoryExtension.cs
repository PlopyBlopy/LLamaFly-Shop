using Core.Interfaces;
using Infrastructure.DataBase.Repositories;

namespace API.Extensions
{
    public static class AppRepositoryExtension
    {
        public static IServiceCollection AddAppRepositoriesServices(this IServiceCollection services)
        {
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICategoryRepository, CategoryRespository>();

            return services;
        }
    }
}