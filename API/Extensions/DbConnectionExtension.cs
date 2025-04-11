using Infrastructure.DataBase.Connections;
using Infrastructure.DataBase.Interfaces;

namespace API.Extensions
{
    public static class DbConnectionExtension
    {
        public static IServiceCollection AddDbConnectionsServices(this IServiceCollection services, IConfiguration configuration)
        {
            var dbConnectionString = configuration.GetConnectionString("DbConnectionString");

            if (string.IsNullOrEmpty(dbConnectionString))
                throw new ArgumentException("DataBase connection string is missing");

            services.AddSingleton<IDbConnectionFactory>(_ => new NpgsqlDbConnectionFactory(dbConnectionString));
            return services;
        }
    }
}