using Core.Contracts.Dtos;
using Core.Interfaces;
using Dapper;
using FluentResults;
using Infrastructure.DataBases.Dapper.Interfaces;

namespace Infrastructure.DataBases.Dapper.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly IDbConnectionFactory _connection;

        public CustomerRepository(IDbConnectionFactory connection)
        {
            _connection = connection;
        }

        public async Task<Result<Guid>> AddConsumerCustomer(UserProfileCustomerCreateDto dto, CancellationToken ct)
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
                            INSERT INTO customers (id, role_id, created_at, updated_at)
                            VALUES (@Id, @RoleId, @CreatedAt, @UpdatedAt)
                            """,
                        new { Id = dto.Customer.ProfileId, RoleId = roleId, CreatedAt = dto.Customer.CreatedAt, UpdatedAt = dto.Customer.UpdatedAt },
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

        public Task<Result<CustomerDto>> GetCustomer(Guid id, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public Task<Result> UpdateCustomer(CustomerUpdateDto dto, CancellationToken ct)
        {
            throw new NotImplementedException();
        }
    }
}