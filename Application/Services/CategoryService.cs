using Application.Utilities;
using AutoMapper;
using Core.Interfaces;
using Core.Models;
using Core.Contracts.Dtos;

namespace Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;
        private readonly IMapper _mapper;
        private readonly BuildCategoryHierarchy _buildCategoryHierarchy;

        public CategoryService(ICategoryRepository repository, IMapper mapper, BuildCategoryHierarchy buildCategoryHierarchy)
        {
            _repository = repository;
            _mapper = mapper;
            _buildCategoryHierarchy = buildCategoryHierarchy;
        }

        public async Task Add(CategoryCreateDto dto, CancellationToken ct)
        {
            Category model = _mapper.Map<Category>(dto);

            await _repository.Add(model, ct);
        }

        public async Task<IEnumerable<CategoryDto>> GetAll(CancellationToken ct)
        {
            var flatList = await _repository.GetAll(ct);

            var result = _buildCategoryHierarchy.BuildHierarchy(flatList);

            return result;
        }

        public async Task Delete(Guid id, CancellationToken ct)
        {
            await _repository.Delete(id, ct);
        }
    }
}