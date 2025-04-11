using Microsoft.Extensions.Caching.Distributed;
using StackExchange.Redis;

namespace API.BackgroundServices
{
    public class CacheTTLService : BackgroundService
    {
        private readonly IConnectionMultiplexer _redis;
        private readonly IDistributedCache _cache;
        private readonly ILogger<CacheTTLService> _logger;
        private readonly TimeSpan _checkInterval = TimeSpan.FromMinutes(1);
        private const int AccessThreshold = 10; // Минимальное количество обращений
        private const int SlidingWindowMinutes = 60; // Скользящее окно анализа
        private const int BaseTtlMinutes = 10; // Базовый TTL для новых ключей
        private const int ExtendedTtlMinutes = 30; // TTL для популярных ключей

        public CacheTTLService(
            IConnectionMultiplexer redis,
            IDistributedCache cache,
            ILogger<CacheTTLService> logger)
        {
            _redis = redis;
            _cache = cache;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var db = _redis.GetDatabase();

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await UpdateCacheExpiration(db);
                    await CleanOldAccessRecords(db);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error updating TTL");
                }
                await Task.Delay(_checkInterval, stoppingToken);
            }
        }

        private async Task UpdateCacheExpiration(IDatabase db)
        {
            var cutoff = DateTimeOffset.UtcNow.AddMinutes(-SlidingWindowMinutes);

            // Получаем все ключи с количеством обращений
            var allKeys = await db.SortedSetRangeByScoreWithScoresAsync(
                "cache_access",
                //start: cutoff.ToUnixTimeSeconds(),
                start: AccessThreshold,
                stop: double.PositiveInfinity,
                order: Order.Descending);

            foreach (var entry in allKeys)
            {
                var key = (RedisKey)entry.Element.ToString();
                var accessCount = entry.Score;

                // Удаляем ключи с недостаточной популярностью
                if (accessCount < AccessThreshold)
                {
                    await RemoveKey(db, key);
                    continue;
                }

                // Обновляем TTL для популярных ключей
                await UpdateKeyTtl(db, key);
            }
        }

        private async Task RemoveKey(IDatabase db, RedisKey key)
        {
            try
            {
                await db.KeyDeleteAsync(key);
                _logger.LogInformation($"Removed unpopular key: {key}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to remove key: {key}");
            }
        }

        private async Task UpdateKeyTtl(IDatabase db, RedisKey key)
        {
            var currentTtl = await db.KeyTimeToLiveAsync(key);
            var newTtl = TimeSpan.FromMinutes(ExtendedTtlMinutes);

            if (!currentTtl.HasValue || currentTtl.Value < newTtl)
            {
                await db.KeyExpireAsync(key, newTtl);
                _logger.LogDebug($"Updated TTL for key: {key} to {newTtl}");
            }
        }

        private async Task CleanOldAccessRecords(IDatabase db)
        {
            var cutoff = DateTimeOffset.UtcNow.AddMinutes(-SlidingWindowMinutes).ToUnixTimeSeconds();
            await db.SortedSetRemoveRangeByScoreAsync(
                "cache_access",
                0,
                cutoff,
                Exclude.Stop);
        }
    }
}