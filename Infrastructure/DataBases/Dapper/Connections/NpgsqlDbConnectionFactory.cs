using Infrastructure.DataBases.Dapper.Interfaces;
using Npgsql;
using System.Data;

namespace Infrastructure.DataBases.Dapper.Connections
{
    public class NpgsqlDbConnectionFactory : IDbConnectionFactory
    {
        private readonly string _connectionString;

        public NpgsqlDbConnectionFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<IDbConnection> CreateConnectionAsync(CancellationToken ct = default)
        {
            var connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync(ct);
            return connection;
        }
    }
}