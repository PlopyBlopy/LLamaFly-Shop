using Core.Interfaces;
using Core.Models;
using Dapper;
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

        public async Task Add(User model, CancellationToken ct)
        {
            using var connection = await _connection.CreateConnectionAsync(ct);

            var command = new CommandDefinition(
                commandText:
                    """
                    INSERT INTO users (id, role, login, email, phone_number, password)
                    VALUES (@Id, @Role, @Login, @Email, @PhoneNumber, @Password)
                    """,
                parameters: model,
                cancellationToken: ct);

            await connection.ExecuteAsync(command);
        }

        public async Task<User>? Get(string identifier, CancellationToken ct)
        {
            using var connection = await _connection.CreateConnectionAsync(ct);

            var command = new CommandDefinition(
                commandText:
                    """
                    SELECT id, role, login, email, phone_number AS PhoneNumber, password
                    FROM users
                    WHERE login = @identifier OR email = @identifier OR phone_Number = @identifier
                    """,
                parameters: new { identifier },
                cancellationToken: ct);

            var result = await connection.QueryFirstOrDefaultAsync<User>(command);

            return result;
        }
    }
}