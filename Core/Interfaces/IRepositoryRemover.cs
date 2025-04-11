namespace Core.Interfaces
{
    public interface IRepositoryRemover<T>
    {
        Task RemoveByIdAsync(Guid id);

        Task RemoveAsync(T item);
    }
}