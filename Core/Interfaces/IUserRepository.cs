using Core.Models;
using FluentResults;

namespace Core.Interfaces
{
    public interface IUserRepository : IRepository
    {
        Task<Result> Add(User model, CancellationToken ct);

        Task<Result<User>> Get(string identifier, CancellationToken ct);

        Task<Result<User>> GetById(Guid id, CancellationToken ct);
    }
}