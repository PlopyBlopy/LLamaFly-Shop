using Core.Contracts.Dtos;
using Core.Contracts.Requests;
using Core.Models;
using FluentResults;

namespace Core.Interfaces
{
    public interface ICategoryRepository //: IRepository<CategoryEntity, CategoryDto>
    {
        Task<Result> Add(Category model, CancellationToken ct);

        Task<Result> AddRange(List<Category> models, CancellationToken ct);

        Task<Result<CategoryDto>> GetCategory(Guid id, CancellationToken ct);

        Task<Result<IEnumerable<CategoryFlatDto>>> GetCategories(CancellationToken ct);

        Task<Result> UpdateCategory(CategoryUpdateDto dto, CancellationToken ct);

        Task<Result> Delete(Guid id, CancellationToken ct);

        Task<Result<bool>> IsCategoryExist(Guid id, CancellationToken ct);
    }
}