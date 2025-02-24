using Core.Models;
using Core.Contracts.Dtos;

namespace Core.Interfaces
{
    public interface ICategoryRepository //: IRepository<CategoryEntity, CategoryDto>
    {
        Task Add(Category model, CancellationToken ct);

        Task<IEnumerable<CategoryFlatDto>>? GetAll(CancellationToken ct);

        Task Delete(Guid id, CancellationToken ct);
    }
}