using Core.Interfaces;
using Core.Models;
using Dapper;
using Infrastructure.DataBase.Interfaces;
using System.Text;
using Core.Contracts.Dtos;

namespace Infrastructure.DataBase.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IDbConnectionFactory _connection;

        public ProductRepository(IDbConnectionFactory connectionFactory)
        {
            _connection = connectionFactory;
        }

        public async Task Add(Product model, CancellationToken ct)
        {
            using var connection = await _connection.CreateConnectionAsync(ct);

            var command = new CommandDefinition(
                commandText:
                    """
                    INSERT INTO products (id, title, description, price, rating, created_at, seller_id, category_id)
                    VALUES (@Id, @Title, @Description, @Price, @Rating, @CreatedAt, @SellerId, @CategoryId)
                    """,
                parameters: model,
                cancellationToken: ct
                );

            await connection.ExecuteAsync(command);
        }

        public async Task<IEnumerable<ProductCardDto>?> GetCards(ProductFiltersDto dto, CancellationToken ct)
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

            if (dto.CategoryId == null)
            {
                //UNDONE: Исправить (возможно добавить в Json и получать через IOption)
                Guid defaultCategoryId = Guid.Parse("c7b5ef2a-2436-400f-a5d5-d0c62d641eba");

                conditions.Add("category_id = @DefaultCategoryId");
                parameters.Add("DefaultCategoryId", defaultCategoryId);
            }
            else
            {
                conditions.Add("category_id = @CategoryId");
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

            return result;
        }

        public async Task<ProductDto?> GetProductById(Guid id, CancellationToken ct)
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

            var result = await connection.QueryFirstOrDefaultAsync<ProductDto?>(command);

            return result;
        }

        public async Task<ProductSellerDto?> GetProductSellerById(Guid id, CancellationToken ct)
        {
            using var connection = await _connection.CreateConnectionAsync(ct);

            var command = new CommandDefinition(
                commandText:
                    """
                    SELECT id, title, description, price, created_at AS CreatedAt, category_id AS CategoryId
                    FROM products
                    WHERE id = @id
                    """,
                parameters: new { id },
                cancellationToken: ct);

            var result = await connection.QueryFirstOrDefaultAsync<ProductSellerDto?>(command);

            return result;
        }

        public async Task Update(ProductUpdateDto dto, CancellationToken ct)
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
                query.Append(" SET ");
                query.Append(string.Join(", ", conditions));
            }
            else
            {
                // TODO: Обратка в кастомном Exceptions от IExceptionHandler
                throw new NullReferenceException("The update data fields are empty.");
            }

            query.Append(" WHERE ");
            query.Append("id = @Id");
            parameters.Add("Id", dto.Id);

            var command = new CommandDefinition(
                commandText: query.ToString(),
                parameters: parameters,
                cancellationToken: ct);

            await connection.QueryAsync(command);
        }

        public async Task Delete(Guid id, CancellationToken ct)
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
        }
    }
}