using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataBase.Interfaces
{
    public interface IProductServiceDbContext : IDbContext
    {
        public DbSet<ProductEntity> Products { get; set; }
        public DbSet<CategoryEntity> Categories { get; set; }
    }
}