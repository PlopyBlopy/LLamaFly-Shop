using Core.Models;
using Core.Contracts.Dtos;

namespace Core.Interfaces
{
    public interface IProductRepository // : IRepository<ProductEntity, ProductDto>
    {
        Task Add(Product model, CancellationToken ct);

        Task<IEnumerable<ProductCardDto>?> GetCards(ProductFiltersDto dto, CancellationToken ct);

        Task<ProductDto?> GetProductById(Guid id, CancellationToken ct);

        Task<ProductSellerDto?> GetProductSellerById(Guid id, CancellationToken ct);

        Task Update(ProductUpdateDto dto, CancellationToken ct);

        Task Delete(Guid id, CancellationToken ct);
    }
}