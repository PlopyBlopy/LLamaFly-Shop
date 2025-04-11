using Core.Contracts.Messages;
using Core.Contracts.Requests;
using FluentResults;
using static Core.Interfaces.Constraints.IUserConstraints;

namespace Core.Interfaces
{
    public interface IUserService : IService
    {
        Task<Result<UserCreateMessage>> AddUser(UserRole role, UserRegisterRequest request, CancellationToken ct);

        Task<Result> UpdateUser();
    }
}