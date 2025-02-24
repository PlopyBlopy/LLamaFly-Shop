using Core.Entities;
using Core.Models;

namespace Core.Interfaces
{
    public interface IUserRepository : IRepository
    {
        Task Add(User model, CancellationToken ct);

        Task<User>? Get(string identifier, CancellationToken ct);
    }
}