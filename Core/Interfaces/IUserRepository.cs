using Core.Contracts.Dtos;
using FluentResults;

namespace Core.Interfaces
{
    public interface IUserRepository
    {
        Task<Result<UserDto>> GetUser(Guid id, CancellationToken ct);

        Task<Result> UpdateUser(UserUpdateDto dto, CancellationToken ct);

        Task<Result> DeleteUser(Guid id, CancellationToken ct);
    }
}