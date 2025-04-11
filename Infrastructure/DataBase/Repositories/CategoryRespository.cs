using Core.Contracts.Dtos;
using Core.Contracts.Requests;
using Core.Extensions.Errors;
using Core.Interfaces;
using Core.Models;
using Dapper;
using FluentResults;
using Infrastructure.DataBase.Interfaces;
using System.Text;

namespace Infrastructure.DataBase.Repositories
{
    public class CategoryRespository : ICategoryRepository
    {
        private readonly IDbConnectionFactory _connection;

        public CategoryRespository(IDbConnectionFactory connectionFactory)
        {
            _connection = connectionFactory;
        }

        public async Task<Result> Add(Category model, CancellationToken ct)
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

            return Result.Ok();
        }

        public async Task<Result> AddRange(List<Category> models, CancellationToken ct)
        {
            using var connection = await _connection.CreateConnectionAsync(ct);

            var queryBuilder = new StringBuilder("INSERT INTO categories (id, title, parent_category_id) VALUES ");

            var parameters = new DynamicParameters();

            for (int i = 0; i < models.Count; i++)
            {
                var model = models[i];

                queryBuilder.Append($"(@Id{i}, @Title{i}, @ParentCategoryId{i})");

                parameters.Add($"Id{i}", model.Id);
                parameters.Add($"Title{i}", model.Title);
                parameters.Add($"ParentCategoryId{i}", model.ParentCategoryId);

                if (i != models.Count - 1)
                {
                    queryBuilder.Append(',');
                }
            }

            var command = new CommandDefinition(
                commandText: queryBuilder.ToString(),
                parameters: parameters,
                cancellationToken: ct);

            await connection.ExecuteAsync(command);

            return Result.Ok();
        }

        public async Task<Result<CategoryDto>> GetCategory(Guid id, CancellationToken ct)
        {
            using var connection = await _connection.CreateConnectionAsync(ct);
            var command = new CommandDefinition(
                commandText:
                    """
                    SELECT id, title, parent_category_id AS ParentCategoryId
                    FROM categories
                    WHERE id = @id
                    """,
                parameters: new { id },
                cancellationToken: ct);

            var result = await connection.QueryFirstOrDefaultAsync<CategoryDto?>(command);
            return result is null ? Result.Fail(new NotFoundError("Category", "Category", id)) : Result.Ok(result);
        }

        public async Task<Result<IEnumerable<CategoryFlatDto>>> GetCategories(CancellationToken ct)
        {
            using var connection = await _connection.CreateConnectionAsync(ct);

            var command = new CommandDefinition(
                commandText:
                """
                WITH RECURSIVE CategoryTree AS (
                    SELECT id, title, parent_category_id AS ParentCategoryId, 1 AS Level
                    FROM categories
                    WHERE parent_category_id IS NULL

                    UNION ALL

                    SELECT c.id, c.title, c.parent_category_id, ct.Level + 1
                    FROM categories c
                    INNER JOIN CategoryTree ct ON c.parent_category_id = ct.id
                )
                SELECT * FROM CategoryTree
                """,
                cancellationToken: ct);

            var flatList = await connection.QueryAsync<CategoryFlatDto>(command);

            return Result.Ok(flatList);
        }

        public async Task<Result> UpdateCategory(CategoryUpdateDto dto, CancellationToken ct)
        {
            using var connection = await _connection.CreateConnectionAsync(ct);
            var query = new StringBuilder("UPDATE categories");
            var conditions = new List<string>();
            var parameters = new DynamicParameters();

            if (!string.IsNullOrEmpty(dto.Title))
            {
                conditions.Add("title = @Title");
                parameters.Add("Title", dto.Title);
            }

            if (dto.ParentCategoryId != null)
            {
                conditions.Add("parent_category_id = @ParentCategoryId");
                parameters.Add("ParentCategoryId", dto.ParentCategoryId);
            }

            if (conditions.Count > 0)
            {
                //conditions.Add("updated_at = @UpdatedAt");
                //parameters.Add("UpdatedAt", dto.UpdatedAt);

                query.Append(" SET ");
                query.Append(string.Join(", ", conditions));
            }
            else
            {
                return Result.Fail(new NotNullError("Product.Update", "Product", dto.Id));
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

        public async Task<Result> Delete(Guid id, CancellationToken ct)
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

            return Result.Ok();
        }

        public async Task<Result<bool>> IsCategoryExist(Guid id, CancellationToken ct)
        {
            using var connection = await _connection.CreateConnectionAsync(ct);
            var command = new CommandDefinition(
                commandText:
                    """
                    SELECT id FROM categories
                    WHERE id = @id
                    """,
                parameters: new { id },
                cancellationToken: ct);

            var categoryId = await connection.QueryFirstOrDefaultAsync<Guid?>(command);

            return categoryId is null ? Result.Fail<bool>(new NotFoundError("Category", "Category", id)) : Result.Ok(true);
        }
    }
}