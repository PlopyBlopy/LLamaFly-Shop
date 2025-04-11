using Core.Contracts.Dtos;
using Core.Interfaces;
using Dapper;
using FluentResults;
using Infrastructure.DataBases.Dapper.Interfaces;

namespace Infrastructure.DataBases.Dapper.Repositories
{
    public class SellerRepository : ISellerRepository
    {
        private readonly IDbConnectionFactory _connection;

        public SellerRepository(IDbConnectionFactory connection)
        {
            _connection = connection;
        }

        public async Task<Result<Guid>> AddConsumerSeller(UserProfileSellerCreateDto dto, CancellationToken ct)
        {
            using var connection = await _connection.CreateConnectionAsync(ct);

            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    var roleId = Guid.NewGuid();

                    await connection.ExecuteAsync(
                            """
                            INSERT INTO users (id, login, email, phone_number, created_at, updated_at)
                            VALUES (@Id, @Login, @Email, @PhoneNumber, @CreatedAt, @UpdatedAt);
                            """,
                        new { Id = dto.User.Id, Login = dto.User.Login, Email = dto.User.Email, PhoneNumber = dto.User.PhoneNumber, CreatedAt = dto.User.CreatedAt, UpdatedAt = dto.User.UpdatedAt },
                        transaction);

                    await connection.ExecuteAsync(
                            """
                            INSERT INTO roles (id, user_id, type, created_at, updated_at)
                            VALUES (@Id, @UserId, @Type, @CreatedAt, @UpdatedAt)
                            """,
                        new { Id = roleId, UserId = dto.User.Id, Type = dto.User.Role, CreatedAt = dto.User.CreatedAt, UpdatedAt = dto.User.UpdatedAt },
                        transaction);

                    await connection.ExecuteAsync(
                            """
                            INSERT INTO profiles (id, user_id, name, surname, patronymic, created_at, updated_at)
                            VALUES (@Id, @UserId, @Name, @Surname, @Patronymic, @CreatedAt, @UpdatedAt)
                            """,
                        new { Id = dto.Profile.Id, UserId = dto.User.Id, Name = dto.Profile.Name, dto.Profile.Surname, dto.Profile.Patronymic, CreatedAt = dto.User.CreatedAt, UpdatedAt = dto.User.UpdatedAt },
                        transaction);

                    await connection.ExecuteAsync(
                            """
                            INSERT INTO sellers (id, role_id, created_at, updated_at)
                            VALUES (@Id, @RoleId, @CreatedAt, @UpdatedAt)
                            """,
                        new { Id = dto.Seller.ProfileId, RoleId = roleId, CreatedAt = dto.Seller.CreatedAt, UpdatedAt = dto.Seller.UpdatedAt },
                        transaction);

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return Result.Fail(ex.Message);
                }
            }

            return Result.Ok();
        }

        public async Task<Result<SellerDto>> GetSeller(Guid id, CancellationToken ct)
        {
            using var connection = await _connection.CreateConnectionAsync(ct);

            var command = new CommandDefinition(
                commandText:
                    """
                    SELECT name, surname, patronymic
                    FROM profiles
                    WHERE user_id = @id
                    """,
                parameters: new { id },
                cancellationToken: ct);

            var result = await connection.QueryFirstOrDefaultAsync<SellerDto>(command);

            return result is null ? Result.Fail("The seller was not found") : Result.Ok(result);
        }

        public Task<Result> UpdateSeller(SellerUpdateDto dto, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<bool>> IsExist(Guid id, CancellationToken ct)
        {
            using var connection = await _connection.CreateConnectionAsync();

            var command = new CommandDefinition(
                commandText:
                """
                SELECT id FROM profiles
                WHERE id = @id
                """,
                parameters: new { id },
                cancellationToken: ct);

            var result = await connection.QueryFirstOrDefaultAsync<Guid>(command);

            return result != null;
        }
    }
}