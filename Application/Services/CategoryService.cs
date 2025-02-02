using AutoMapper;
using Core.Contracts.Dto;
using Core.Entities;
using Core.Interfaces;
using Core.Models;

namespace Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<(bool, string)> Add(CategoryCreateDto dto, CancellationToken ct)
        {
            Category model = _mapper.Map<Category>(dto);

            CategoryEntity entity = _mapper.Map<CategoryEntity>(model);

            return await _repository.Add(entity, ct);
        }

        public async Task Delete(Guid id, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<CategoryDto?>> GetAll(CancellationToken ct)
        {
            IEnumerable<CategoryDto>? dtos = await _repository.GetAll(ct);
            return dtos;
        }

        public async Task<CategoryDto> GetById(Guid id, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public async Task Update(CategoryDto dto, CancellationToken ct)
        {
            throw new NotImplementedException();
        }
    }
}