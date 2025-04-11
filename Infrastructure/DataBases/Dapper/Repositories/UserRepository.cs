using Core.Extensions.Errors;
using Core.Interfaces;
using Core.Models;
using Dapper;
using FluentResults;
using Infrastructure.DataBases.Dapper.Interfaces;

namespace Infrastructure.DataBases.Dapper.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IDbConnectionFactory _connection;

        public UserRepository(IDbConnectionFactory connectionFactory)
        {
            _connection = connectionFactory;
        }

        public async Task<Result> Add(User model, CancellationToken ct)
        {
            using var connection = await _connection.CreateConnectionAsync(ct);

            var command = new CommandDefinition(
                commandText:
                    """
                    INSERT INTO users (id, role, login, email, phone_number, password, created_at, updated_at)
                    VALUES (@Id, @Role, @Login, @Email, @PhoneNumber, @Password, @CreatedAt, @UpdatedAt)
                    """,
                parameters: model,
                cancellationToken: ct);

            await connection.ExecuteAsync(command);

            return Result.Ok();
        }

        public async Task<Result<User>> Get(string identifier, CancellationToken ct)
        {
            using var connection = await _connection.CreateConnectionAsync(ct);

            var command = new CommandDefinition(
                commandText:
                    """
                    SELECT id, role, login, email, phone_number AS PhoneNumber, password, created_at AS CreatedAt, updated_At AS UpdatedAt
                    FROM users
                    WHERE login = @identifier OR email = @identifier OR phone_Number = @identifier
                    """,
                parameters: new { identifier },
                cancellationToken: ct);

            var result = await connection.QueryFirstOrDefaultAsync<User>(command);

            return result is null ? Result.Fail(new NotFoundError("User", "User")) : Result.Ok(result);
        }

        public async Task<Result<User>> GetById(Guid id, CancellationToken ct)
        {
            using var connection = await _connection.CreateConnectionAsync(ct);

            var command = new CommandDefinition(
                commandText:
                    """
                    SELECT id, role, login, email, phone_number AS PhoneNumber, password, created_at AS CreatedAt, updated_At AS UpdatedAt
                    FROM users
                    WHERE id = @id
                    """,
                parameters: new { id },
                cancellationToken: ct);

            var result = await connection.QueryFirstOrDefaultAsync<User>(command);

            return result is null ? Result.Fail(new NotFoundError("User", "User", id)) : Result.Ok(result);
        }
    }
}