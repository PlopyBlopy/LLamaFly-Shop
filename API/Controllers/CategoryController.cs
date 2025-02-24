using AutoMapper;
using Core.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Core.Contracts.Requests;
using Microsoft.AspNetCore.Authorization;
using Core.Contracts.Dtos;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _service;
        private readonly IMapper _mapper;
        private readonly IValidator<CategoryCreateRequest> _validator;

        public CategoryController(ICategoryService service, IMapper mapper, IValidator<CategoryCreateRequest> validator)
        {
            _service = service;
            _mapper = mapper;
            _validator = validator;
        }

        [Authorize(Policy = "Admin")]
        [HttpPost("add")]
        public async Task<ActionResult> Add([FromBody] CategoryCreateRequest request, CancellationToken ct)
        {
            var validationResult = _validator.Validate(request);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            CategoryCreateDto dto = _mapper.Map<CategoryCreateDto>(request);

            await _service.Add(dto, ct);

            return Created();
        }

        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetAll(CancellationToken ct)
        {
            var result = await _service.GetAll(ct);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [Authorize(Policy = "Admin")]
        [HttpDelete("delete")]
        public async Task<ActionResult> Delete([FromQuery] Guid id, CancellationToken ct)
        {
            await _service.Delete(id, ct);

            return Ok();
        }
    }
}