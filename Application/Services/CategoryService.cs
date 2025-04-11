using Application.Utilities;
using AutoMapper;
using Core.Contracts.Dtos;
using Core.Contracts.Requests;
using Core.Extensions.Errors;
using Core.Interfaces;
using Core.Models;
using FluentResults;
using FluentValidation;
using Infrastructure.Cache;
using Microsoft.Extensions.Caching.Distributed;

namespace Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IDistributedCache _cache;
        private readonly ICategoryRepository _repository;
        private readonly IValidator<CategoryCreateRequest> _createValidator;
        private readonly IValidator<CategoryUpdateRequest> _updateValidator;
        private readonly IMapper _mapper;
        private readonly BuildCategoryHierarchy _buildCategoryHierarchy;

        private const string categories = "categories-";
        private const string categoriesAll = "category-all";

        public CategoryService(IDistributedCache cache, ICategoryRepository repository, IValidator<CategoryCreateRequest> createValidator, IValidator<CategoryUpdateRequest> updateValidator, IMapper mapper, BuildCategoryHierarchy buildCategoryHierarchy)
        {
            _cache = cache;
            _repository = repository;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
            _mapper = mapper;
            _buildCategoryHierarchy = buildCategoryHierarchy;
        }

        public async Task<Result> AddCategory(CategoryCreateRequest request, CancellationToken ct)
        {
            var validationResult = _createValidator.Validate(request);

            if (!validationResult.IsValid)
            {
                var fieldErrors = validationResult.Errors
                    .Select(e => new ValidationFieldError(
                        message: e.ErrorMessage,
                        errorCode: e.ErrorCode,
                        propertyName: e.PropertyName,
                        attemptedValue: e.AttemptedValue))
                    .ToList();

                return Result.Fail(new ValidationError("Category.Create", fieldErrors));
            }

            Category model = _mapper.Map<Category>(request);

            var result = await _repository.Add(model, ct);

            if (result.IsFailed)
                return result;

            await _cache.RemoveAsync(categoriesAll);

            return Result.Ok();
        }

        public async Task<Result> AddCategoryRange(IEnumerable<CategoryCreateRequest> request, CancellationToken ct)
        {
            List<List<IError>> validationResults = new List<List<IError>>();

            foreach (var category in request)
            {
                var validationResult = _createValidator.Validate(category);

                if (!validationResult.IsValid)
                {
                    var fieldErrors = validationResult.Errors
                        .Select(e => (IError)new ValidationFieldError(
                            message: e.ErrorMessage,
                            errorCode: e.ErrorCode,
                            propertyName: e.PropertyName,
                            attemptedValue: e.AttemptedValue))
                        .ToList();

                    validationResults.Add(fieldErrors);
                }
            }

            if (validationResults.Any())
            {
                return Result.Fail(new ValidationRangeError("Categories.Create", validationResults));
            }

            List<Category> models = new List<Category>(request.Count());

            foreach (var category in request)
            {
                var model = _mapper.Map<Category>(category);
                models.Add(model);
            }

            return await _repository.AddRange(models, ct);
        }

        public async Task<Result<CategoryDto>> GetCategory(Guid id, CancellationToken ct)
        {
            return await _cache.GetResultAsync(
                $"{categories}{id}",
                async ct => await _repository.GetCategory(id, ct),
                options: null,
                ct);
        }

        public async Task<Result<IEnumerable<CategoryHierarchyDto>>> GetCategories(CancellationToken ct)
        {
            return await _cache.GetAsync<List<CategoryHierarchyDto>>(categoriesAll,
                async getMethod => _buildCategoryHierarchy.BuildHierarchy((await _repository.GetCategories(ct)).Value),
                options: null,
                ct);
            //TODO: _cache.GetAsync но версия с Result для BuildHierarchy
            //return await _cache.GetResultAsync(
            //    $"categories-all",
            //    async ct => _buildCategoryHierarchy.BuildHierarchy((await _repository.GetCategories(ct)).Value),
            //    options: null,
            //    ct);
        }

        public async Task<Result> UpdateCategory(CategoryUpdateRequest request, CancellationToken ct)
        {
            var validationResult = _updateValidator.Validate(request);

            if (!validationResult.IsValid)
            {
                var fieldErrors = validationResult.Errors
                    .Select(e => new ValidationFieldError(
                        message: e.ErrorMessage,
                        errorCode: e.ErrorCode,
                        propertyName: e.PropertyName,
                        attemptedValue: e.AttemptedValue))
                    .ToList();

                return Result.Fail(new ValidationError("Category.Create", fieldErrors));
            }

            var dto = _mapper.Map<CategoryUpdateDto>(request);

            var result = await _repository.UpdateCategory(dto, ct);

            if (result.IsFailed)
                return result;
            else
            {
                await _cache.RemoveAsync($"{categories}{request.Id}");
                await _cache.RemoveAsync($"{categoriesAll}");
            }

            return Result.Ok();
        }

        public async Task<Result> DeleteCategory(Guid id, CancellationToken ct)
        {
            var result = await _repository.Delete(id, ct);

            if (result.IsFailed)
                return result;
            else
            {
                await _cache.RemoveAsync($"{categories}{id}");
                await _cache.RemoveAsync($"{categoriesAll}");
            }

            return Result.Ok();
        }

        public async Task<Result<bool>> IsCategoryExists(Guid id, CancellationToken ct)
        {
            return await IsCategoryExists(id, ct);
        }
    }
}