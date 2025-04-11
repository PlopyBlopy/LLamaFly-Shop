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
    [Route("api/products/categories")]
    public class CategoriesController : ControllerBase, ICategoryController
    {
        private readonly ICategoryService _service;
        private readonly IMapper _mapper;
        private readonly IErrorFactoryHandler _errorFactoryHandler;
        private readonly IValidator<CategoryCreateRequest> _validator;

        public CategoriesController(ICategoryService service, IMapper mapper, IErrorFactoryHandler errorFactoryHandler, IValidator<CategoryCreateRequest> validator)
        {
            _service = service;
            _mapper = mapper;
            _errorFactoryHandler = errorFactoryHandler;
            _validator = validator;
        }

        [Authorize(Policy = "admin")]
        [HttpPost]
        public async Task<ActionResult> AddCategory([FromBody] CategoryCreateRequest request, CancellationToken ct)
        {
            var result = await _service.AddCategory(request, ct);

            return result.IsSuccess
                ? Created()
                : _errorFactoryHandler.GetHandler(result.Errors.First()).Handle(result.Errors.First());
        }

        [Authorize(Policy = "admin")]
        [HttpPost("range")]
        public async Task<ActionResult> AddCategories([FromBody] IEnumerable<CategoryCreateRequest> requests, CancellationToken ct)
        {
            var result = await _service.AddCategoryRange(requests, ct);

            return result.IsSuccess
                ? Ok()
                : _errorFactoryHandler.GetHandler(result.Errors.First()).Handle(result.Errors.First());
        }

        [Authorize(Policy = "admin")]
        [HttpGet]
        public async Task<ActionResult<CategoryHierarchyDto>> GetCategory([FromQuery] Guid id, CancellationToken ct)
        {
            var result = await _service.GetCategory(id, ct);

            return result.IsSuccess
                ? Ok(result.Value)
                : _errorFactoryHandler.GetHandler(result.Errors.First()).Handle(result.Errors.First());
        }

        [HttpGet("range")]
        public async Task<ActionResult<IEnumerable<CategoryHierarchyDto>>> GetCategories(CancellationToken ct)
        {
            var result = await _service.GetCategories(ct);

            return result.IsSuccess
                ? Ok(result.Value)
                : _errorFactoryHandler.GetHandler(result.Errors.First()).Handle(result.Errors.First());
        }

        [Authorize(Policy = "admin")]
        [HttpPut]
        public async Task<IActionResult> UpdateCategory([FromBody] CategoryUpdateRequest request, CancellationToken ct)
        {
            var result = await _service.UpdateCategory(request, ct);

            return result.IsSuccess
                ? Ok()
                : _errorFactoryHandler.GetHandler(result.Errors.First()).Handle(result.Errors.First());
        }

        [Authorize(Policy = "admin")]
        [HttpDelete]
        public async Task<IActionResult> DeleteCategory([FromQuery] Guid id, CancellationToken ct)
        {
            var result = await _service.DeleteCategory(id, ct);

            return result.IsSuccess
                ? Ok()
                : _errorFactoryHandler.GetHandler(result.Errors.First()).Handle(result.Errors.First());
        }

        [Authorize(Policy = "admin")]
        [HttpGet("isexist")]
        public async Task<ActionResult<bool>> IsCategoryExists(Guid id, CancellationToken ct)
        {
            var result = await _service.IsCategoryExists(id, ct);

            return result.IsSuccess
                ? Ok(result.Value)
                : _errorFactoryHandler.GetHandler(result.Errors.First()).Handle(result.Errors.First());
        }
    }
}