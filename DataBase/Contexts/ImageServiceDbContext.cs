using Core.Entities;
using DataBase.Configurations;
using DataBase.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataBase.Contexts
{
    public class ImageServiceDbContext : DbContext, IImageServiceDbContext
    {
        public DbSet<ProductEntity> Products { get; set; }

        public ImageServiceDbContext(DbContextOptions<ImageServiceDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            base.OnModelCreating(modelBuilder);
        }

        public async Task<int> SaveChangesAsync(CancellationToken ct)
        {
            return await base.SaveChangesAsync(ct);
        }
    }
}