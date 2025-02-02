using Core.Contracts.Dto;
using Core.Entities;

namespace Core.Interfaces
{
    public interface IProductRepository : IRepository<ProductEntity, ProductDto>
    {
        Task<IEnumerable<ProductCardDto>?> GetCards(ProductFiltersDto dto, CancellationToken ct);
    }
}