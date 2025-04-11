using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataBase.Interfaces
{
    public interface IImageServiceDbContext : IDbContext
    {
        public DbSet<ProductEntity> Products { get; set; }
    }
}