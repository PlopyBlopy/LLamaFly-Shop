namespace Core.Interfaces
{
    public interface IService<TDto, TCreateDto>
        where TDto : class
        where TCreateDto : class
    {
        Task<(bool, string)> Add(TCreateDto dto, CancellationToken ct);

        Task<TDto?> GetById(Guid id, CancellationToken ct);

        Task<IEnumerable<TDto>?> GetAll(CancellationToken ct);

        Task Update(TDto dto, CancellationToken ct);

        Task Delete(Guid id, CancellationToken ct);
    }
}