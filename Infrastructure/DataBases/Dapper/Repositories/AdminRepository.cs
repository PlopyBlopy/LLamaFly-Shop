using Core.Contracts.Dtos;
using Core.Interfaces;
using Dapper;
using FluentResults;
using Infrastructure.DataBases.Dapper.Interfaces;

namespace Infrastructure.DataBases.Dapper.Repositories
{
    public class AdminRepository : IAdminRepository
    {
        private readonly IDbConnectionFactory _connection;

        public AdminRepository(IDbConnectionFactory connection)
        {
            _connection = connection;
        }

        public async Task<Result<Guid>> AddConsumerAdmin(UserProfileAdminCreateDto dto, CancellationToken ct)
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
                        new { Id = dto.Profile.Id, UserId = dto.User.Id, Name = dto.Profile.Name, dto.Profile.Surname, dto.Profile.Patronymic, CreatedAt = dto.Profile.CreatedAt, UpdatedAt = dto.Profile.UpdatedAt },
                        transaction);

                    await connection.ExecuteAsync(
                            """
                            INSERT INTO admins (id, role_id, created_at, updated_at)
                            VALUES (@Id, @RoleId, @CreatedAt, @UpdatedAt)
                            """,
                        new { Id = dto.Admin.ProfileId, RoleId = roleId, CreatedAt = dto.Admin.CreatedAt, UpdatedAt = dto.Admin.UpdatedAt },
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

        public Task<Result<AdminDto>> GetAdmin(Guid id, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public Task<Result> UpdateAdmin(AdminUpdateDto dto, CancellationToken ct)
        {
            throw new NotImplementedException();
        }
    }
}