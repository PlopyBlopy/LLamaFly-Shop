using BenchmarkDotNet.Attributes;
using Infrastructure.DataBase.Connections;
using Infrastructure.DataBase.Interfaces;
using Infrastructure.DataBase.Repositories;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;

namespace Tests.PerformanceTests.Infrastructure
{
    [MemoryDiagnoser]
    internal class ProductRepositoryGetProductByIdTest
    {
        private ProductRepository _repository;
        private IServiceProvider _serviceProvider;
        private Guid productId;

        [GlobalSetup]
        public void Setup()
        {
            var services = new ServiceCollection();

            services.AddTransient<IDbConnectionFactory>(_ => new NpgsqlDbConnectionFactory("Server=localhost;Port=5432;Database=ProductServiceDB;User Id=admin;Password=12345;"));

            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = "localhost:6379";
                options.InstanceName = "ProductsCache";
            });

            services.AddTransient<ProductRepository>();

            _serviceProvider = services.BuildServiceProvider();

            _repository = _serviceProvider.GetRequiredService<ProductRepository>();
            Guid productId = Guid.Parse("1c4007e8-7aa4-462b-9995-02838d5a3f02");
        }

        [Benchmark]
        public async Task GetProductById()
        {
            var product = await _repository.GetProduct(productId, default);
        }

        [IterationCleanup]
        public void ClearCache()
        {
            var cache = _serviceProvider.GetRequiredService<IDistributedCache>();
            cache.Remove($"product-{productId}");
        }

        [GlobalCleanup]
        public void Cleanup()
        {
            (_serviceProvider as IDisposable)?.Dispose();
        }
    }
}