using Core.Interfaces;

namespace API.Extensions
{
    public static class AppRepositoryExtensions
    {
        public static IServiceCollection AddAppRepositoriesServices(this IServiceCollection services)
        {
            //services.AddScoped<IProductRepository, ProductRepository>();

            return services;
        }
    }
}