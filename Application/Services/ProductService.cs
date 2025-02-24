using AutoMapper;
using Core.Interfaces;
using Core.Models;
using Core.Contracts.Dtos;

namespace Application.Services
{
    public sealed class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task Add(ProductCreateDto dto, CancellationToken ct)
        {
            Product model = _mapper.Map<Product>(dto);
            await _repository.Add(model, ct);
        }

        public async Task<IEnumerable<ProductCardDto>?> GetCards(ProductFiltersDto dto, CancellationToken ct)
        {
            var result = await _repository.GetCards(dto, ct);
            return result;
        }

        public async Task<ProductDto?> GetProductById(Guid id, CancellationToken ct)
        {
            var result = await _repository.GetProductById(id, ct);
            return result;
        }

        public async Task<ProductSellerDto?> GetProductSellerById(Guid id, CancellationToken ct)
        {
            var result = await _repository.GetProductSellerById(id, ct);
            return result;
        }

        public async Task Update(ProductUpdateDto dto, CancellationToken ct)
        {
            await _repository.Update(dto, ct);
        }

        public async Task Delete(Guid id, CancellationToken ct)
        {
            await _repository.Delete(id, ct);
        }
    }
}