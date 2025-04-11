using API.BackgroundServices;
using Infrastructure.Cache;
using Microsoft.Extensions.Caching.Distributed;
using StackExchange.Redis;

namespace API.Extensions
{
    public static class CacheExtension
    {
        public static IServiceCollection AddCacheServices(this IServiceCollection services, IConfiguration configuration)
        {
            var redisConnectionString = configuration.GetConnectionString("Redis");

            if (string.IsNullOrEmpty(redisConnectionString))
                throw new ArgumentException("Redis connection string is missing");

            services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(redisConnectionString));
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = redisConnectionString;
                options.InstanceName = "products-service-";
            });
            services
                .Decorate<IDistributedCache, TrackingDistributedCache>();

            services.AddHostedService<CacheTTLService>();

            return services;
        }
    }
}