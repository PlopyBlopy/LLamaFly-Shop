using Core.Contracts.Dtos;
using Core.Contracts.Requests;
using Microsoft.AspNetCore.Mvc;

namespace API.Interfaces
{
    public interface IProductController
    {
        Task<ActionResult> AddProduct([FromBody] ProductCreateRequest request, CancellationToken ct);

        Task<ActionResult> AddProductRange(IEnumerable<ProductCreateRequest> requests, CancellationToken ct);

        Task<ActionResult> AddProductRange(IEnumerable<ProductCreateWithRatingRequest> requests, CancellationToken ct);

        Task<ActionResult<IEnumerable<ProductCardDto>>> GetProductsCards([FromQuery] ProductFiltersRequest request, CancellationToken ct);

        Task<ActionResult<IEnumerable<ProductCardDto>>> GetSellerProductsCards([FromRoute] Guid id, CancellationToken ct);

        //Task<ActionResult<ProductDetailDto>> GetProduct(Guid id, CancellationToken ct);

        //Task<ActionResult<ProductDetailDto>> GetProduct([FromRoute] Guid id, CancellationToken ct);

        Task<ActionResult<ProductDetailDto>> GetProduct([FromQuery] Guid productId, CancellationToken ct);

        Task<ActionResult<ProductSellerDto>> GetSellerProduct([FromRoute] Guid productId, CancellationToken ct);

        Task<IActionResult> UpdateProduct([FromBody] ProductUpdateRequest request, CancellationToken ct);

        Task<IActionResult> DeleteProduct([FromRoute] Guid id, CancellationToken ct);
    }
}