using Core.Contracts.Dtos;
using Core.Interfaces;
using Core.Models;
using Dapper;
using Infrastructure.DataBases.Dapper.Interfaces;

namespace Infrastructure.DataBases.Dapper.Repositories
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly IDbConnectionFactory _connection;

        public RefreshTokenRepository(IDbConnectionFactory connectionFactory)
        {
            _connection = connectionFactory;
        }

        public async Task Add(RefreshToken model, CancellationToken ct)
        {
            var connection = await _connection.CreateConnectionAsync(ct);

            var command = new CommandDefinition(
                commandText:
                """
                INSERT INTO refresh_tokens (id, user_id, token, expires, is_revoked, created_at)
                VALUES (@Id, @UserId, @Token, @Expires, @IsRevoked, @CreatedAt)
                """,
                parameters: model,
                cancellationToken: ct);

            await connection.ExecuteAsync(command);
        }

        public async Task<RefreshToken?> Get(RefreshTokenDto dto, CancellationToken ct)
        {
            var connection = await _connection.CreateConnectionAsync(ct);

            var command = new CommandDefinition(
                commandText:
                """
                SELECT id, user_id AS UserId, token, expires, is_revoked AS IsRevoked, created_at AS CreatedAt
                FROM refresh_tokens
                WHERE token = @RefreshToken
                """,
                parameters: dto,
                cancellationToken: ct);

            var result = await connection.QueryFirstOrDefaultAsync<RefreshToken>(command);
            return result;
        }

        public async Task Revoke(RefreshTokenDto token, CancellationToken ct)
        {
            var connection = await _connection.CreateConnectionAsync(ct);

            var command = new CommandDefinition(
                commandText:
                """
                UPDATE refresh_tokens
                SET is_revoked = TRUE
                WHERE token = @RefreshToken
                """,
                parameters: token,
                cancellationToken: ct);

            await connection.ExecuteAsync(command);
        }
    }
}