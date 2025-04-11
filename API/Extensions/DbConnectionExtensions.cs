using Infrastructure.DataBases.Dapper.Connections;
using Infrastructure.DataBases.Dapper.Interfaces;

namespace API.Extensions
{
    public static class DbConnectionExtensions
    {
        public static IServiceCollection AddDbConnectionsServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IDbConnectionFactory>(_ => new NpgsqlDbConnectionFactory(configuration["DbConnectionString"]!));
            return services;
        }
    }
}