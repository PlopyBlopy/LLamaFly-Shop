using Core.Contracts.Dtos;
using Core.Models;

namespace Core.Interfaces
{
    public interface IRefreshTokenRepository : IRepository
    {
        Task Add(RefreshToken model, CancellationToken ct);

        Task<RefreshToken?> Get(RefreshTokenDto dto, CancellationToken ct);

        Task Revoke(RefreshTokenDto dto, CancellationToken ct);
    }
}