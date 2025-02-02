using DataBase.Contexts;
using DataBase.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Services.Extensions
{
    public static class DbContextExtensions
    {
        public static IServiceCollection AddDbContextServices(this IServiceCollection services)
        {
            services.AddScoped<IProductServiceDbContext, ProductServiceDbContext>();

            return services;
        }

        public static IServiceCollection AddDbContextConfigurations(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ProductServiceDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("ProductServiceConnectionString")));

            return services;
        }
    }
}