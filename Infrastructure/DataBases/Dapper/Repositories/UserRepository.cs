using Core.Contracts.Dtos;
using Core.Interfaces;
using FluentResults;
using Infrastructure.DataBases.Dapper.Interfaces;

namespace Infrastructure.DataBases.Dapper.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IDbConnectionFactory _connection;

        public UserRepository(IDbConnectionFactory connection)
        {
            _connection = connection;
        }

        public Task<Result<UserDto>> GetUser(Guid id, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public Task<Result> UpdateUser(UserUpdateDto dto, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public Task<Result> DeleteUser(Guid id, CancellationToken ct)
        {
            throw new NotImplementedException();
        }
    }
}