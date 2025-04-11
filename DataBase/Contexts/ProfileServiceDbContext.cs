using Core.Entities;
using DataBase.Configurations;
using DataBase.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataBase.Contexts
{
    public class ProfileServiceDbContext : DbContext, IProfileServiceDbContext
    {
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<SellerEntity> Sellers { get; set; }
        public DbSet<CustomerEntity> Customers { get; set; }

        public ProfileServiceDbContext(DbContextOptions<ProfileServiceDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new SellerConfiguration());
            modelBuilder.ApplyConfiguration(new CustomerConfiguration());

            base.OnModelCreating(modelBuilder);
        }

        public Task<int> SaveChangesAsync(CancellationToken ct)
        {
            throw new NotImplementedException();
        }
    }
}