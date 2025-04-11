using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataBase.Interfaces
{
    public interface IProfileServiceDbContext
    {
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<SellerEntity> Sellers { get; set; }
        public DbSet<CustomerEntity> Customers { get; set; }
    }
}