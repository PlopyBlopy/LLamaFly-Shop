using API.Interfaces;
using AutoMapper;
using Core.Contracts.Dtos;
using Core.Contracts.Requests;
using Core.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _service;
        private readonly IMapper _mapper;
        private readonly IErrorFactoryHandler _errorFactoryHandler;
        private readonly IValidator<ProductCreateRequest> _validator;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(IProductService service, IMapper mapper, IErrorFactoryHandler errorFactoryHandler, IValidator<ProductCreateRequest> validator, ILogger<ProductsController> logger)
        {
            _service = service;
            _mapper = mapper;
            _errorFactoryHandler = errorFactoryHandler;
            _validator = validator;
            _logger = logger;
        }

        [Authorize(Policy = "seller")]
        [HttpPost]
        public async Task<ActionResult<Guid>> AddProduct([FromBody] ProductCreateRequest request, CancellationToken ct)
        {
            //TODO: При добавлении продукта, обновить cache (удалить)
            var result = await _service.AddProduct(request, ct);

            return result.IsSuccess
                ? Ok(result.Value)
                : _errorFactoryHandler.GetHandler(result.Errors.First()).Handle(result.Errors.First());
        }

        [Authorize(Policy = "admin")]
        [HttpPost("range")]
        public async Task<ActionResult> AddProductRange(IEnumerable<ProductCreateRequest> requests, CancellationToken ct)
        {
            var result = await _service.AddProductRange(requests, ct);

            return result.IsSuccess
                ? Created()
                : _errorFactoryHandler.GetHandler(result.Errors.First()).Handle(result.Errors.First());
        }

        [Authorize(Policy = "admin")]
        [HttpPost("range/withrating")]
        public async Task<ActionResult> AddProductRange(IEnumerable<ProductCreateWithRatingRequest> requests, CancellationToken ct)
        {
            var result = await _service.AddProductRange(requests, ct);

            return result.IsSuccess
                ? Created()
                : _errorFactoryHandler.GetHandler(result.Errors.First()).Handle(result.Errors.First());
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductCardDto>>> GetProductsCards([FromQuery] ProductFiltersRequest request, CancellationToken ct)
        {
            ProductFiltersDto dto = _mapper.Map<ProductFiltersDto>(request);

            var result = await _service.GetProductsCards(dto, ct);

            return result.IsSuccess
                ? Ok(result.Value)
                : _errorFactoryHandler.GetHandler(result.Errors.First()).Handle(result.Errors.First());
        }

        [Authorize(Policy = "seller")]
        [HttpGet("profile/seller/{id}")]
        public async Task<ActionResult<IEnumerable<ProductCardDto>>> GetSellerProductsCards([FromRoute] Guid id, CancellationToken ct)
        {
            var result = await _service.GetSellerProductsCards(id, ct);

            return result.IsSuccess
                ? Ok(result.Value)
                : _errorFactoryHandler.GetHandler(result.Errors.First()).Handle(result.Errors.First());
        }

        [HttpGet("detail/{productId}")]
        public async Task<ActionResult<ProductDetailDto>> GetProduct([FromRoute] Guid productId, CancellationToken ct)
        {
            var result = await _service.GetProduct(productId, ct);

            return result.IsSuccess
                ? Ok(result.Value)
                : _errorFactoryHandler.GetHandler(result.Errors.First()).Handle(result.Errors.First());
        }

        [Authorize(Policy = "seller")]
        [HttpGet("seller/{productId}")]
        public async Task<ActionResult<ProductSellerDto>> GetSellerProduct([FromRoute] Guid productId, CancellationToken ct)
        {
            var result = await _service.GetSellerProduct(productId, ct);

            return result.IsSuccess
                ? Ok(result.Value)
                : _errorFactoryHandler.GetHandler(result.Errors.First()).Handle(result.Errors.First());
        }

        [Authorize(Policy = "seller")]
        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateProduct([FromBody] ProductUpdateRequest request, CancellationToken ct)
        {
            var result = await _service.UpdateProduct(request, ct);

            return result.IsSuccess
                ? Ok()
                : _errorFactoryHandler.GetHandler(result.Errors.First()).Handle(result.Errors.First());
        }

        [Authorize(Policy = "seller")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct([FromRoute] Guid id, CancellationToken ct)
        {
            var result = await _service.DeleteProduct(id, ct);

            return result.IsSuccess
                ? Ok()
                : _errorFactoryHandler.GetHandler(result.Errors.First()).Handle(result.Errors.First());
        }
    }
}