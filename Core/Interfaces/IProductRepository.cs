using Core.Contracts.Dtos;
using Core.Models;
using FluentResults;

namespace Core.Interfaces
{
    public interface IProductRepository
    {
        Task<Result<Guid>> AddProduct(Product model, CancellationToken ct);

        Task<Result> AddProductRange(List<Product> models, CancellationToken ct);

        Task<Result<IEnumerable<ProductCardDto>>> GetProductsCards(ProductFiltersDto dto, CancellationToken ct);

        Task<Result<IEnumerable<ProductCardDto>>> GetSellerProductsCards(Guid id, CancellationToken ct);

        Task<Result<ProductDetailDto>> GetProduct(Guid id, CancellationToken ct);

        Task<Result<ProductSellerDto>> GetSellerProduct(Guid id, CancellationToken ct);

        Task<Result> UpdateProduct(ProductUpdateDto dto, CancellationToken ct);

        Task<Result> DeleteProduct(Guid id, CancellationToken ct);
    }
}