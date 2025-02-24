using Core.Models;

namespace Core.Interfaces
{
    public interface ITokenService : IService
    {
        string GenerateToken(User user);
    }
}