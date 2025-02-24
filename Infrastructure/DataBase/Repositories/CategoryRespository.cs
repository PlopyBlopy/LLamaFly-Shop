using Core.Interfaces;
using Core.Models;
using Dapper;
using Infrastructure.DataBase.Interfaces;
using Core.Contracts.Dtos;

namespace Infrastructure.DataBase.Repositories
{
    public class CategoryRespository : ICategoryRepository
    {
        private readonly IDbConnectionFactory _connection;

        public CategoryRespository(IDbConnectionFactory connectionFactory)
        {
            _connection = connectionFactory;
        }

        public async Task Add(Category model, CancellationToken ct)
        {
            using var connection = await _connection.CreateConnectionAsync();

            var command = new CommandDefinition(
                commandText:
                    """
                    INSERT INTO categories (id, title, parent_category_id)
                    VALUES (@Id, @Title, @ParentCategoryId)
                    """,
                parameters: model,
                cancellationToken: ct);

            await connection.ExecuteAsync(command);
        }

        public async Task<IEnumerable<CategoryFlatDto>> GetAll(CancellationToken ct)
        {
            using var connection = await _connection.CreateConnectionAsync(ct);

            var command = new CommandDefinition(
                commandText:
                """
                WITH RECURSIVE CategoryTree AS (
                    SELECT id, title, parent_category_id AS ParentCategoryId,
                    1 AS Level
                    FROM categories
                    WHERE parent_category_id IS NULL

                    UNION ALL

                    SELECT c.id, c.title, c.parent_category_id,
                    ct.Level + 1
                    FROM categories c
                    INNER JOIN CategoryTree ct ON c.parent_category_id = ct.id
                )
                SELECT * FROM CategoryTree
                """,
                cancellationToken: ct);

            var flatList = await connection.QueryAsync<CategoryFlatDto>(command);

            return flatList;
        }

        public async Task Delete(Guid id, CancellationToken ct)
        {
            using var connection = await _connection.CreateConnectionAsync(ct);
            var command = new CommandDefinition(
                commandText:
                    """
                    DELETE FROM categories
                    WHERE id = @id
                    """,
                parameters: new { id },
                cancellationToken: ct);

            await connection.ExecuteAsync(command);
        }
    }
}