using Infrastructure.DataBase.Interfaces;
using Npgsql;
using System.Data;

namespace Infrastructure.DataBase.Connections
{
    public class NpgsqlDbConnectionFactory : IDbConnectionFactory
    {
        private readonly string _connectionString;

        public NpgsqlDbConnectionFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<IDbConnection> CreateConnectionAsync(CancellationToken token = default)
        {
            var connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync(token);
            return connection;
        }
    }
}