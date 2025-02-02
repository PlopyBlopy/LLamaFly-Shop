using Core.Interfaces;
using DataBase.Repositories;

namespace API.Services.Extensions
{
    public static class AppRepositoryExtensions
    {
        public static IServiceCollection AddAppRepositoriesServices(this IServiceCollection services)
        {
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();

            return services;
        }
    }
}