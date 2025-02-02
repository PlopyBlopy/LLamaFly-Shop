using API.Contracts.Requests;
using API.Contracts.Responses;
using AutoMapper;
using Core.Contracts.Dto;
using Core.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _service;
        private readonly IMapper _mapper;
        private readonly IValidator<ProductCreateDto> _validator;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(IProductService service, IMapper mapper, IValidator<ProductCreateDto> validator, ILogger<ProductsController> logger)
        {
            _service = service;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpPost("add")]
        public async Task<ActionResult> Add([FromBody] ProductCreateRequest request, CancellationToken ct)
        {
            ProductCreateDto dto = _mapper.Map<ProductCreateDto>(request);

            var validationResult = await _validator.ValidateAsync(dto, ct);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            (bool, string) result = await _service.Add(dto, ct);

            return Ok();
        }

        //[HttpGet("GetById/{id}")]
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductResponse>?> GetById([FromRoute] Guid id, CancellationToken ct) // delete [FromQuery]
        {
            ProductDto dto = await _service.GetById(id, ct);

            if (dto == null)
            {
                return NotFound();
            }

            ProductResponse response = _mapper.Map<ProductResponse>(dto);

            return Ok(response);
        }

        [HttpGet("cards")]
        public async Task<ActionResult<IEnumerable<ProductCardDto>?>> GetCards([FromQuery] ProductFiltersRequest request, CancellationToken ct)
        {
            //_logger.LogError("Запрос на получение данных ОТКЛОНЕН");

            ProductFiltersDto dto = _mapper.Map<ProductFiltersDto>(request);

            IEnumerable<ProductCardDto>? response = await _service.GetCards(dto, ct);

            return Ok(response);
        }

        //[HttpGet("GetAll")]
        //public async Task<ActionResult<IEnumerable<ProductResponse>>?> GetAll(CancellationToken ct)
        //{
        //    var response = Enumerable.Empty<ProductResponse>();
        //    return Ok(response);
        //}

        //[HttpPut("Update")]
        //public async Task<ActionResult> Update([FromBody] ProductRequest request, CancellationToken ct)
        //{
        //    return Ok(null);
        //}

        [HttpDelete("delete/{ids}")]
        public async Task<ActionResult> Delete([FromRoute] Guid id, CancellationToken ct)
        {
            return Ok();
        }
    }
}