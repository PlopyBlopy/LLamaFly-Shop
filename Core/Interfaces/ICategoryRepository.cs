using Core.Contracts.Dto;
using Core.Entities;

namespace Core.Interfaces
{
    public interface ICategoryRepository : IRepository<CategoryEntity, CategoryDto>
    {
        public Task<CategoryEntity?> GetOnlyParentById(Guid parentCategoryId, CancellationToken ct);
    }
}