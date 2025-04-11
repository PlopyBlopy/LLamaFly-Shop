using Core.Contracts.Dtos;
using Core.Contracts.Requests;
using FluentResults;

namespace Core.Interfaces
{
    public interface ICategoryService : IService
    {
        Task<Result> AddCategory(CategoryCreateRequest request, CancellationToken ct);

        Task<Result> AddCategoryRange(IEnumerable<CategoryCreateRequest> request, CancellationToken ct);

        Task<Result<CategoryDto>> GetCategory(Guid id, CancellationToken ct);

        Task<Result<IEnumerable<CategoryHierarchyDto>>> GetCategories(CancellationToken ct);

        Task<Result> UpdateCategory(CategoryUpdateRequest request, CancellationToken ct);

        Task<Result> DeleteCategory(Guid id, CancellationToken ct);

        Task<Result<bool>> IsCategoryExists(Guid id, CancellationToken ct);
    }
}