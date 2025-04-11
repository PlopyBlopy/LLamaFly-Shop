using Core.Contracts.Dtos;
using Core.Contracts.Requests;
using FluentResults;

namespace Core.Interfaces
{
    public interface IUserService : IService
    {
        Task<Result<UserDto>> GetUser(Guid id, CancellationToken ct);

        Task<Result> UpdateUser(Guid id, UserUpdateRequest request, CancellationToken ct);

        Task<Result> DeleteUser(Guid id, CancellationToken ct);
    }
}