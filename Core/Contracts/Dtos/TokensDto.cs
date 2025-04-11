using Core.Models;

namespace Core.Contracts.Dtos
{
    public record TokensDto(string AccessToken, RefreshToken RefreshToken);
}