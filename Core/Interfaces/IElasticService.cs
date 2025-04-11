using Core.Models;

namespace Core.Interfaces
{
    public interface IElasticService
    {
        //Task CreateIndexIfNotExist(string index);

        Task<bool> AddOrUpdate(Product product);
    }
}