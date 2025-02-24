using AutoMapper;
using Core.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Core.Contracts.Requests;
using Core.Contracts.Responses;
using Core.Contracts.Dtos;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _service;
        private readonly IMapper _mapper;
        private readonly IValidator<ProductCreateRequest> _validator;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(IProductService service, IMapper mapper, IValidator<ProductCreateRequest> validator, ILogger<ProductsController> logger)
        {
            _service = service;
            _mapper = mapper;
            _validator = validator;
            _logger = logger;
        }

        [Authorize(Policy = "Seller")]

        //[Authorize(Roles = "Seller")]
        [HttpPost("add")]
        public async Task<ActionResult> Add([FromBody] ProductCreateRequest request, CancellationToken ct)
        {
            //return new BadRequestResult();
            //throw new ValidationException("Product controller error");

            var validationResult = _validator.Validate(request);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            ProductCreateDto dto = _mapper.Map<ProductCreateDto>(request);

            await _service.Add(dto, ct);

            return Ok();
        }

        [HttpGet("cards")]
        public async Task<ActionResult<IEnumerable<ProductCardDto>?>> GetCards([FromQuery] ProductFiltersRequest request, CancellationToken ct)
        {
            ProductFiltersDto dto = _mapper.Map<ProductFiltersDto>(request);

            var response = await _service.GetCards(dto, ct);

            return Ok(response);
        }

        [HttpGet("product")]
        public async Task<ActionResult<ProductResponse>?> GetProductById([FromQuery] Guid id, CancellationToken ct)
        {
            var result = await _service.GetProductById(id, ct);

            var response = _mapper.Map<ProductResponse>(result);

            return Ok(response);
        }

        [Authorize(Policy = "Seller")]

        //[Authorize(Roles = "Seller")]
        [HttpGet("seller-product")]
        public async Task<ActionResult<ProductSellerResponse>?> GetSellerProductById([FromQuery] Guid id, CancellationToken ct)
        {
            var result = await _service.GetProductSellerById(id, ct);

            var response = _mapper.Map<ProductSellerResponse>(result);

            return Ok(response);
        }

        [Authorize(Policy = "Seller")]

        //[Authorize(Roles = "Seller")]
        [HttpPatch("update")]
        public async Task<ActionResult> Update([FromBody] ProductUpdateRequest request, CancellationToken ct)
        {
            ProductUpdateDto dto = _mapper.Map<ProductUpdateDto>(request);

            await _service.Update(dto, ct);

            return Ok();
        }

        [Authorize(Policy = "Seller")]

        //[Authorize(Roles = "Seller")]
        [HttpDelete("delete")]
        public async Task<ActionResult> Delete([FromRoute] Guid id, CancellationToken ct)
        {
            await _service.Delete(id, ct);

            return Ok();
        }
    }
}