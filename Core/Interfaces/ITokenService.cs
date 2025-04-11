using Core.Contracts.Dtos;
using Core.Models;

namespace Core.Interfaces
{
    public interface ITokenService : IService
    {
        string GenerateAccessToken(User model);

        RefreshToken GenerateRefreshToken(User model);

        Task Add(RefreshToken model, CancellationToken ct);

        //Task<TokensDto> GetRefreshToken(string token, CancellationToken ct);
        Task<TokensDto> GetRefreshToken(RefreshTokenDto token, CancellationToken ct);

        Task Revoke(RefreshTokenDto token, CancellationToken ct);
    }
}