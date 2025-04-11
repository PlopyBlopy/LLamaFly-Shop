namespace Core.Interfaces
{
    public interface IRepositoryWriter<T>
    {
        Task SaveAsync(T item);

        Task SaveAllAsync(List<T> items);

        Task UpdateAsync(T item);

        Task UpdateAllAsync(List<T> items);
    }
}