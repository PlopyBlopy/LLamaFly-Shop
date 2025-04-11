using DataBase.Contexts;
using DataBase.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions
{
    public static class DbContextExtensions
    {
        public static IServiceCollection AddDbContextServices(this IServiceCollection services)
        {
            services.AddScoped<IImageServiceDbContext, ImageServiceDbContext>();

            return services;
        }

        public static IServiceCollection AddDbContextConfigurations(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ImageServiceDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("ProductServiceConnectionString")));

            return services;
        }
    }
}