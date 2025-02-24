using Infrastructure.DataBase.Connections;
using Infrastructure.DataBase.Interfaces;

namespace API.Extensions
{
    public static class DbConnectionExtension
    {
        public static IServiceCollection AddDbConnectionsServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IDbConnectionFactory>(_ => new NpgsqlDbConnectionFactory(configuration["DbConnectionString"]!));
            return services;
        }
    }
}