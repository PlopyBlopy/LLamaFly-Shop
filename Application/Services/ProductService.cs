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
using System.Text;

namespace Application.Services
{
    public sealed class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
        private readonly IDistributedCache _cache;
        private readonly IProfileService _profileService;
        private readonly IValidator<ProductCreateRequest> _createValidator;
        private readonly IValidator<ProductCreateWithRatingRequest> _createWithRatingValidator;
        private readonly IValidator<ProductUpdateRequest> _updateValidator;
        private readonly IMapper _mapper;

        private const string products = "products-";
        private const string productsCards = "products-cards-";
        private const string profileSeller = "seller-";

        public ProductService(IProductRepository repository, IDistributedCache cache, IProfileService profileService, IValidator<ProductCreateRequest> createValidator, IValidator<ProductCreateWithRatingRequest> createWithRatingValidator, IValidator<ProductUpdateRequest> updateValidator, IMapper mapper)
        {
            _repository = repository;
            _cache = cache;
            _profileService = profileService;
            _createValidator = createValidator;
            _createWithRatingValidator = createWithRatingValidator;
            _updateValidator = updateValidator;
            _mapper = mapper;
        }

        public async Task<Result<Guid>> AddProduct(ProductCreateRequest request, CancellationToken ct)
        {
            var validationResult = await _createValidator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                var fieldErrors = validationResult.Errors
                    .Select(e => new ValidationFieldError(
                        message: e.ErrorMessage,
                        errorCode: e.ErrorCode,
                        propertyName: e.PropertyName,
                        attemptedValue: e.AttemptedValue))
                    .ToList();

                return Result.Fail(new ValidationError("Product.Create", fieldErrors));
            }

            //var isSellerExist = await _profileService.IsSellerExist(request.SellerId, ct);

            //if (!isSellerExist)
            //    return Result.Fail(new NotFoundError("Profile.Seller", "Seller", request.SellerId));

            Product model = _mapper.Map<Product>(request);

            var result = await _repository.AddProduct(model, ct);

            if (result.IsFailed)
                return result;
            else
            {
                await _cache.RemoveAsync($"{productsCards}");
                await _cache.RemoveAsync($"{productsCards}{profileSeller}{request.SellerId}");
            }

            return result;
        }

        public async Task<Result> AddProductRange(IEnumerable<ProductCreateRequest> request, CancellationToken ct)
        {
            List<List<IError>> validationResults = new List<List<IError>>();

            foreach (var product in request)
            {
                var validationResult = _createValidator.Validate(product);

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
                return Result.Fail(new ValidationRangeError("Products.Create", validationResults));
            }

            //TODO: Проверка на наличие seller
            //TODO: изменить модель на формат id и массив товаров

            List<Product> models = new List<Product>(request.Count());

            foreach (var product in request)
            {
                var model = _mapper.Map<Product>(product);
                models.Add(model);
            }

            return await _repository.AddProductRange(models, ct);
        }

        public async Task<Result> AddProductRange(IEnumerable<ProductCreateWithRatingRequest> request, CancellationToken ct)
        {
            List<List<IError>> validationResults = new List<List<IError>>();

            foreach (var product in request)
            {
                var validationResult = _createWithRatingValidator.Validate(product);

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
                return Result.Fail(new ValidationRangeError("Products.Create", validationResults));
            }

            //TODO: Проверка на наличие seller
            //TODO: изменить модель на формат id и массив товаров

            List<Product> models = new List<Product>(request.Count());

            foreach (var product in request)
            {
                var model = _mapper.Map<Product>(product);
                models.Add(model);
            }

            return await _repository.AddProductRange(models, ct);
        }

        public async Task<Result<IEnumerable<ProductCardDto>>> GetProductsCards(ProductFiltersDto dto, CancellationToken ct)
        {
            //TODO: Исправить, опртимизировать (при удалении продукта или добавлении или изменении нужно обновлять все фильтры)
            StringBuilder query = new StringBuilder();
            List<string> conditions = new List<string>();

            if (!string.IsNullOrEmpty(dto.Search))
                conditions.Add(dto.Search);
            if (dto.CategoryId != null || dto.CategoryId != Guid.Empty)
                conditions.Add(dto.CategoryId.ToString());
            if (!string.IsNullOrEmpty(dto.SortProp))
                conditions.Add(dto.SortProp);
            if (!string.IsNullOrEmpty(dto.SortOrder))
                conditions.Add(dto.SortOrder);

            if (conditions.Any())
                query.Append(string.Join('-', conditions));

            return await _cache.GetResultAsync(
                $"{productsCards}{query.ToString()}",
                async ct => await _repository.GetProductsCards(dto, ct),
                options: null,
                ct);
        }

        public async Task<Result<IEnumerable<ProductCardDto>>> GetSellerProductsCards(Guid id, CancellationToken ct)
        {
            return await _cache.GetResultAsync(
                $"{productsCards}{profileSeller}{id}",
                async ct => await _repository.GetSellerProductsCards(id, ct),
                options: null,
                ct);
        }

        public async Task<Result<ProductDetailDto>> GetProduct(Guid id, CancellationToken ct)
        {
            return await _cache.GetResultAsync(
                $"{products}{id}",
                async ct => await _repository.GetProduct(id, ct),
                options: null,
                ct);
        }

        public async Task<Result<ProductSellerDto>> GetSellerProduct(Guid id, CancellationToken ct)
        {
            return await _cache.GetResultAsync(
                $"{products}{profileSeller}{id}",
                async ct => await _repository.GetSellerProduct(id, ct),
                options: null,
                ct);
        }

        public async Task<Result> UpdateProduct(ProductUpdateRequest request, CancellationToken ct)
        {
            var validationResult = await _updateValidator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                var fieldErrors = validationResult.Errors
                    .Select(e => new ValidationFieldError(
                        message: e.ErrorMessage,
                        errorCode: e.ErrorCode,
                        propertyName: e.PropertyName,
                        attemptedValue: e.AttemptedValue))
                    .ToList();

                return Result.Fail(new ValidationError("ValidationError", fieldErrors));
            }
            ProductUpdateDto dto = _mapper.Map<ProductUpdateDto>(request);

            var result = await _repository.UpdateProduct(dto, ct);

            if (result.IsFailed)
                return result;
            else
            {
                await _cache.RemoveAsync($"{products}{request.Id}");
                await _cache.RemoveAsync($"{productsCards}");
            }

            return Result.Ok();
        }

        public async Task<Result> DeleteProduct(Guid id, CancellationToken ct)
        {
            var result = await _repository.DeleteProduct(id, ct);

            if (result.IsFailed)
                return result;
            else
            {
                await _cache.RemoveAsync($"{products}{id}");
                await _cache.RemoveAsync($"{productsCards}");
            }

            return Result.Ok();
        }
    }
}