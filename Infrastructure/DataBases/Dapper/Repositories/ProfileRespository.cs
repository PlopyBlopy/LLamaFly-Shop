using Core.Contracts.Dtos;
using Core.Extensions.Errors;
using Core.Interfaces;
using Dapper;
using FluentResults;
using Infrastructure.DataBases.Dapper.Interfaces;
using System.Text;

namespace Infrastructure.DataBases.Dapper.Repositories
{
    public class ProfileRespository : IProfileRepository
    {
        private readonly IDbConnectionFactory _connection;

        public ProfileRespository(IDbConnectionFactory dbConnection)
        {
            _connection = dbConnection;
        }

        public async Task<Result<ProfileDto>> GetProfile(Guid id, CancellationToken ct)
        {
            using var connection = await _connection.CreateConnectionAsync(ct);

            var command = new CommandDefinition(
                commandText:
                    """
                    SELECT name, surname, patronymic FROM profiles
                    WHERE id = @id
                    """,
                parameters: new { id },
                cancellationToken: ct);

            var result = await connection.QueryFirstOrDefaultAsync<ProfileDto>(command);

            return result is null ? Result.Fail<ProfileDto>(new NotFoundError("Profile", "Profile", id)) : Result.Ok(result);
        }

        public async Task<Result> UpdateProfile(ProfileUpdateDto dto, CancellationToken ct)
        {
            using var connection = await _connection.CreateConnectionAsync(ct);

            var query = new StringBuilder("UPDATE profiles");
            var conditions = new List<string>();
            var parameters = new DynamicParameters();

            if (!string.IsNullOrEmpty(dto.Name))
            {
                conditions.Add("name = @Name");
                parameters.Add("Name", dto.Name);
            }

            if (!string.IsNullOrEmpty(dto.Surname))
            {
                conditions.Add("surname = @Surname");
                parameters.Add("Surname", dto.Surname);
            }

            if (!string.IsNullOrEmpty(dto.Patronymic))
            {
                conditions.Add("patronymic = @Patronymic");
                parameters.Add("Patronymic", dto.Patronymic);
            }

            if (conditions.Count > 0)
            {
                conditions.Add("updated_at = @UpdatedAt");
                parameters.Add("UpdatedAt", dto.UpdatedAt);

                query.Append(" SET ");
                query.Append(string.Join(", ", conditions));
            }
            else
            {
                return Result.Fail(new NotNullError("Profiles.Update", "Profiles", dto.Id));
            }

            query.Append(" WHERE ");
            query.Append("id = @Id");
            parameters.Add("Id", dto.Id);

            var command = new CommandDefinition(
                commandText: query.ToString(),
                parameters: parameters,
                cancellationToken: ct);

            await connection.QueryAsync(command);

            return Result.Ok();
        }
    }
}