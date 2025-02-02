using Microsoft.Extensions.Configuration;

namespace DataBase.Interfaces
{
    public interface IDbContext
    {
        Task<int> SaveChangesAsync(CancellationToken ct);
    }
}