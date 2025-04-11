using Core.Contracts.Dtos;
using Core.Extensions.Errors;
using Core.Interfaces;
using Core.Models;
using Dapper;
using FluentResults;
using Infrastructure.DataBase.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using System.Text;

namespace Infrastructure.DataBase.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IDbConnectionFactory _connection;
        private readonly IDistributedCache _distributedCache;
        private readonly ILogger<ProductRepository> _logger;

        public ProductRepository(IDbConnectionFactory connectionFactory, IDistributedCache distributedCache, ILogger<ProductRepository> logger)
        {
            _connection = connectionFactory;
            _distributedCache = distributedCache;
            _logger = logger;
        }

        public async Task<Result<Guid>> AddProduct(Product model, CancellationToken ct)
        {
            if (model == null)
                return Result.Fail(new NotNullError("Product", "Product"));

            using var connection = await _connection.CreateConnectionAsync(ct);

            var command = new CommandDefinition(
                commandText:
                    """
                    INSERT INTO products (id, title, description, price, rating, category_id, seller_id, updated_at, created_at)
                    VALUES (@Id, @Title, @Description, @Price, @Rating, @CategoryId, @SellerId, @UpdatedAt, @CreatedAt)
                    RETURNING id
                    """,
                parameters: model,
                cancellationToken: ct
                );

            var productId = await connection.ExecuteScalarAsync<Guid?>(command);

            return productId is null ? Result.Fail(new NotFoundError("CreatedProduct", "productId")) : Result.Ok(productId.Value);
        }

        public async Task<Result> AddProductRange(List<Product> models, CancellationToken ct)
        {
            using var connection = await _connection.CreateConnectionAsync(ct);

            var queryBuilder = new StringBuilder("INSERT INTO products (id, title, description, price, rating, category_id, seller_id, updated_at, created_at) VALUES ");

            var parameters = new DynamicParameters();

            for (int i = 0; i < models.Count; i++)
            {
                var model = models[i];

                queryBuilder.Append($"(@Id{i}, @Title{i}, @Description{i}, @Price{i}, @Rating{i}, @CategoryId{i}, @SellerId{i}, @UpdatedAt{i}, @CreatedAt{i})");

                parameters.Add($"Id{i}", model.Id);
                parameters.Add($"Title{i}", model.Title);
                parameters.Add($"Description{i}", model.Description);
                parameters.Add($"Price{i}", model.Price);
                parameters.Add($"Rating{i}", model.Rating);
                parameters.Add($"CategoryId{i}", model.CategoryId);
                parameters.Add($"SellerId{i}", model.SellerId);
                parameters.Add($"UpdatedAt{i}", model.UpdatedAt);
                parameters.Add($"CreatedAt{i}", model.CreatedAt);

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

        public async Task<Result<IEnumerable<ProductCardDto>>> GetProductsCards(ProductFiltersDto dto, CancellationToken ct)
        {
            using var connection = await _connection.CreateConnectionAsync(ct);
            var query = new StringBuilder("SELECT id, title, price, rating FROM products");
            var conditions = new List<string>();
            var parameters = new DynamicParameters();

            if (!string.IsNullOrEmpty(dto.Search))
            {
                conditions.Add("title ILIKE @Search");
                parameters.Add("Search", $"%{dto.Search}%");
            }

            if (dto.CategoryId != null)
            {
                var cte = @"
                    WITH RECURSIVE recursive_categories AS (
                        SELECT id FROM categories WHERE id = @CategoryId
                        UNION ALL
                        SELECT c.id FROM categories c
                        INNER JOIN recursive_categories rc ON c.parent_category_id = rc.id
                    ) ";
                query.Insert(0, cte);
                conditions.Add("category_id IN (SELECT id FROM recursive_categories)");
                parameters.Add("CategoryId", dto.CategoryId);
            }

            if (conditions.Count > 0)
            {
                query.Append(" WHERE ");
                query.Append(string.Join(" AND ", conditions));
            }

            var orderBy = new StringBuilder(" ORDER BY ");
            var sortProp = !string.IsNullOrEmpty(dto.SortProp)
                ? dto.SortProp.ToLower()
                : "rating";

            var allowedColumns = new HashSet<string> { "price", "rating" };
            if (!allowedColumns.Contains(sortProp))
                sortProp = "rating";

            orderBy.Append(sortProp);

            if (string.IsNullOrEmpty(dto.SortOrder) || dto.SortOrder.ToLower() == "asc")
                orderBy.Append(" ASC ");
            else
                orderBy.Append(" DESC ");

            query.Append(orderBy);

            var command = new CommandDefinition(
                commandText: query.ToString(),
                parameters: parameters,
                cancellationToken: ct
            );

            var result = await connection.QueryAsync<ProductCardDto>(command);

            return result is null ? Result.Fail<IEnumerable<ProductCardDto>>(new NotFoundError("Products", "Products")) : Result.Ok(result);
        }

        public async Task<Result<IEnumerable<ProductCardDto>>> GetSellerProductsCards(Guid id, CancellationToken ct)
        {
            using var connection = await _connection.CreateConnectionAsync(ct);

            var command = new CommandDefinition(
                commandText:
                    """
                    SELECT id, title, price, rating
                    FROM products
                    WHERE seller_id = @id
                    """,
                parameters: new { id },
                cancellationToken: ct);

            var result = await connection.QueryAsync<ProductCardDto>(command);

            return result.Any() ? Result.Ok(result) : Result.Fail<IEnumerable<ProductCardDto>>(new NotFoundError("Seller.Products", "Seller.Products", id));
        }

        public async Task<Result<ProductDetailDto>> GetProduct(Guid id, CancellationToken ct)
        {
            using var connection = await _connection.CreateConnectionAsync(ct);

            var command = new CommandDefinition(
                commandText:
                    """
                    SELECT id, title, description, price, rating
                    FROM products
                    WHERE id = @id
                    """,
                parameters: new { id },
                cancellationToken: ct);

            var result = await connection.QueryFirstOrDefaultAsync<ProductDetailDto?>(command);

            return Result.Ok(result);
        }

        public async Task<Result<ProductSellerDto>> GetSellerProduct(Guid id, CancellationToken ct)
        {
            using var connection = await _connection.CreateConnectionAsync(ct);

            var command = new CommandDefinition(
                commandText:
                    """
                    SELECT id, title, description, price, category_id AS CategoryId, updated_at AS UpdatedAt, created_at AS CreatedAt
                    FROM products
                    WHERE id = @id
                    """,
                parameters: new { id },
                cancellationToken: ct);

            var result = await connection.QueryFirstOrDefaultAsync<ProductSellerDto?>(command);

            return result is null ? Result.Fail<ProductSellerDto>(new NotFoundError("Seller.Product", "Product", id)) : Result.Ok(result);
        }

        public async Task<Result> UpdateProduct(ProductUpdateDto dto, CancellationToken ct)
        {
            using var connection = await _connection.CreateConnectionAsync(ct);
            var query = new StringBuilder("UPDATE products");
            var conditions = new List<string>();
            var parameters = new DynamicParameters();

            if (!string.IsNullOrEmpty(dto.Title))
            {
                conditions.Add("title = @Title");
                parameters.Add("Title", dto.Title);
            }

            if (!string.IsNullOrEmpty(dto.Description))
            {
                conditions.Add("description = @Description");
                parameters.Add("Description", dto.Description);
            }

            if (dto.Price != null)
            {
                conditions.Add("price = @Price");
                parameters.Add("Price", dto.Price);
            }

            if (dto.CategoryId != null)
            {
                conditions.Add("category_id = @CategoryId");
                parameters.Add("CategoryId", dto.CategoryId);
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

        public async Task<Result> DeleteProduct(Guid id, CancellationToken ct)
        {
            using var connection = await _connection.CreateConnectionAsync(ct);

            var command = new CommandDefinition(
                commandText:
                    """
                    DELETE FROM products
                    WHERE id = @id
                    """,
                parameters: new { id },
                cancellationToken: ct);

            await connection.ExecuteAsync(command);

            return Result.Ok();
        }
    }
}