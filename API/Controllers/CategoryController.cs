using API.Contracts.Requests;
using Application.Validation.Models;
using AutoMapper;
using Core.Contracts.Dto;
using Core.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _service;
        private readonly IMapper _mapper;
        private readonly IValidator<CategoryCreateDto> _validator;

        public CategoryController(ICategoryService service, IMapper mapper, IValidator<CategoryCreateDto> validator)
        {
            _service = service;
            _mapper = mapper;
            _validator = validator;
        }

        [HttpPost("Add")]
        public async Task<ActionResult> Add([FromBody] CategoryCreateRequest request, CancellationToken ct)
        {
            CategoryCreateDto dto = _mapper.Map<CategoryCreateDto>(request);

            var validationResult = _validator.Validate(dto);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            (bool, string) result = await _service.Add(dto, ct);

            if (!result.Item1)
                return NotFound(result.Item2);
            return Created();
        }

        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetAll(CancellationToken ct)
        {
            IEnumerable<CategoryDto>? response = await _service.GetAll(ct);

            if (response == null)
                return NotFound();
            return Ok(response);
        }
    }
}