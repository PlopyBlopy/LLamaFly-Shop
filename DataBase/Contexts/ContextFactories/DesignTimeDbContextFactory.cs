using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DataBase.Contexts.ContextFactories
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ImageServiceDbContext>
    {
        public ImageServiceDbContext CreateDbContext(string[] args)
        {
            // Настройка IConfiguration
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("DataBaseSettings.json")
                .Build();

            // Настройка DbContextOptions
            var optionsBuilder = new DbContextOptionsBuilder<ImageServiceDbContext>();
            optionsBuilder.UseNpgsql(configuration.GetConnectionString("ImageServiceConnectionString"));

            return new ImageServiceDbContext(optionsBuilder.Options);
        }
    }
}