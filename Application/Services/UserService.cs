using Core.Contracts.Dtos;
using Core.Contracts.Requests;
using Core.Interfaces;
using FluentResults;

namespace Application.Services
{
    public class UserService : IUserService
    {
        public Task<Result<UserDto>> GetUser(Guid id, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public Task<Result> UpdateUser(Guid id, UserUpdateRequest request, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public Task<Result> DeleteUser(Guid id, CancellationToken ct)
        {
            throw new NotImplementedException();
        }
    }
}