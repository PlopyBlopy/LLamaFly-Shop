using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace DataBase.Contexts.ContextFactories
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ProfileServiceDbContext>
    {
        public ProfileServiceDbContext CreateDbContext(string[] args)
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("DataBaseSettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<ProfileServiceDbContext>();
            optionsBuilder.UseNpgsql(configuration.GetConnectionString("ProfileServiceConnectionString"));

            return new ProfileServiceDbContext(optionsBuilder.Options);
        }
    }
}