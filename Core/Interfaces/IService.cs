namespace Core.Interfaces
{
    public interface IService<TDto, TUploadDto>
        where TDto : class
        where TUploadDto : class
    {
        Task Add(TUploadDto dto, CancellationToken ct);

        Task<TDto> Get(Guid id, CancellationToken ct);

        Task<IEnumerable<TDto>> GetAll(CancellationToken ct);

        Task Update(TUploadDto dto, CancellationToken ct);

        Task Delete(Guid id, CancellationToken ct);
    }
}