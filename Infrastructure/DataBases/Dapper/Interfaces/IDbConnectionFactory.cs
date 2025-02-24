using System.Data;

namespace Infrastructure.DataBases.Dapper.Interfaces
{
    public interface IDbConnectionFactory
    {
        Task<IDbConnection> CreateConnectionAsync(CancellationToken ct = default);
    }
}