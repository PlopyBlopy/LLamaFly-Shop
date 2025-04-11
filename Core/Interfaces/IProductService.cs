using Core.Contracts.Dtos;
using Core.Contracts.Requests;
using FluentResults;

namespace Core.Interfaces
{
    public interface IProductService : IService
    {
        Task<Result<Guid>> AddProduct(ProductCreateRequest request, CancellationToken ct);

        Task<Result> AddProductRange(IEnumerable<ProductCreateRequest> request, CancellationToken ct);

        Task<Result> AddProductRange(IEnumerable<ProductCreateWithRatingRequest> request, CancellationToken ct);

        Task<Result<IEnumerable<ProductCardDto>>> GetProductsCards(ProductFiltersDto dto, CancellationToken ct);

        Task<Result<IEnumerable<ProductCardDto>>> GetSellerProductsCards(Guid id, CancellationToken ct);

        Task<Result<ProductDetailDto>> GetProduct(Guid id, CancellationToken ct);

        Task<Result<ProductSellerDto>> GetSellerProduct(Guid id, CancellationToken ct);

        Task<Result> UpdateProduct(ProductUpdateRequest request, CancellationToken ct);

        Task<Result> DeleteProduct(Guid id, CancellationToken ct);
    }
}