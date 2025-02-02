using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DataBase.Contexts.ContextFactories
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ProductServiceDbContext>
    {
        public ProductServiceDbContext CreateDbContext(string[] args)
        {
            // Настройка IConfiguration
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("DataBaseSettings.json")
                .Build();

            // Настройка DbContextOptions
            var optionsBuilder = new DbContextOptionsBuilder<ProductServiceDbContext>();
            optionsBuilder.UseNpgsql(configuration.GetConnectionString("ProductServiceConnectionString"));

            return new ProductServiceDbContext(optionsBuilder.Options);
        }
    }
}