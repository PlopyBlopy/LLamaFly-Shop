using AutoMapper;
using Core.Contracts.Dto;
using Core.Entities;
using Core.Interfaces;
using Core.Models;

namespace Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<(bool, string)> Add(ProductCreateDto dto, CancellationToken ct)
        {
            Product model = _mapper.Map<Product>(dto);
            ProductEntity entity = _mapper.Map<ProductEntity>(model);
            return await _repository.Add(entity, ct);
        }

        public async Task<ProductDto?> GetById(Guid id, CancellationToken ct)
        {
            ProductDto dto = await _repository.GetById(id, ct);
            return dto;
        }

        public async Task<IEnumerable<ProductDto>> GetAll(CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ProductCardDto>?> GetCards(ProductFiltersDto dto, CancellationToken ct)
        {
            return await _repository.GetCards(dto, ct);
        }

        public async Task Update(ProductDto dto, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public async Task Delete(Guid id, CancellationToken ct)
        {
            throw new NotImplementedException();
        }
    }
}