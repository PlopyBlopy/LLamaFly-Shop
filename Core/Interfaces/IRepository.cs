namespace Core.Interfaces
{
    public interface IRepository<TEntity, TDto>
        where TEntity : class
        where TDto : class
    {
        public Task<(bool, string)> Add(TEntity entity, CancellationToken ct);

        public Task<TDto?> GetById(Guid id, CancellationToken ct);

        public Task<IEnumerable<TDto>?> GetAll(CancellationToken ct);

        public Task Update(TEntity entity, CancellationToken ct);

        public Task Delete(Guid id, CancellationToken ct);
    }
}