using Core.Contracts.Dtos;

namespace Core.Interfaces
{
    public interface ICategoryService // : IService<CategoryDto, CategoryCreateDto>
    {
        Task Add(CategoryCreateDto dto, CancellationToken ct);

        Task<IEnumerable<CategoryDto>> GetAll(CancellationToken ct);

        Task Delete(Guid id, CancellationToken ct);
    }
}