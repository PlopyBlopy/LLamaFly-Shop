using System.Data;

namespace Infrastructure.DataBase.Interfaces
{
    public interface IDbConnectionFactory
    {
        Task<IDbConnection> CreateConnectionAsync(CancellationToken token = default);
    }
}