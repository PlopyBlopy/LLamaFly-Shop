using Core.Contracts.Dto;

namespace Core.Interfaces
{
    public interface IProductService : IService<ProductDto, ProductCreateDto>
    {
        Task<IEnumerable<ProductCardDto>?> GetCards(ProductFiltersDto dto, CancellationToken ct);
    }
}