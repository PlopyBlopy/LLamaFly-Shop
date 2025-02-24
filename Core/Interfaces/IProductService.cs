using Core.Contracts.Requests;
using Core.Contracts.Dtos;

namespace Core.Interfaces
{
    public interface IProductService //: IService<ProductDto, ProductCreateDto>
    {
        Task Add(ProductCreateDto dto, CancellationToken ct);

        Task<IEnumerable<ProductCardDto>?> GetCards(ProductFiltersDto dto, CancellationToken ct);

        Task<ProductDto?> GetProductById(Guid id, CancellationToken ct);

        Task<ProductSellerDto?> GetProductSellerById(Guid id, CancellationToken ct);

        Task Update(ProductUpdateDto dto, CancellationToken ct);

        Task Delete(Guid id, CancellationToken ct);
    }
}