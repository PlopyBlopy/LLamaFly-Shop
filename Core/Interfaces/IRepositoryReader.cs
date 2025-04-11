namespace Core.Interfaces
{
    public interface IRepositoryReader<T>
    {
        Task<T> GetByIdAsync(Guid id);

        Task<T> GetAllAsync();
    }
}