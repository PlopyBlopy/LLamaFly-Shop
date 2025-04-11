using Microsoft.Extensions.Caching.Distributed;
using StackExchange.Redis;

namespace Infrastructure.Cache
{
    public class TrackingDistributedCache : IDistributedCache
    {
        private readonly IDistributedCache _inner;
        private readonly IDatabase _db;

        //TODO: Получать строку из appsettings.json
        private const string InstancePrefix = "image-service-"; // Должно совпадать с настройками

        private string PrefixedKey(string key) => $"{InstancePrefix}{key}";

        public TrackingDistributedCache(IDistributedCache inner, IConnectionMultiplexer redis)
        {
            _inner = inner;
            _db = redis.GetDatabase();
        }

        public void Set(string key, byte[] value, DistributedCacheEntryOptions options)
        {
            _inner.Set(key, value, options);
        }

        public async Task SetAsync(string key, byte[] value, DistributedCacheEntryOptions options, CancellationToken token = default)
        {
            await _inner.SetAsync(key, value, options, token);
        }

        public byte[]? Get(string key)
        {
            TrackAccess(key);
            return _inner.Get(key);
        }

        public async Task<byte[]?> GetAsync(string key, CancellationToken token = default)
        {
            var prefixedKey = PrefixedKey(key);
            await TrackAccessAsync(prefixedKey);
            return await _inner.GetAsync(key, token);
        }

        public void Refresh(string key)
        {
            _inner.Refresh(key);
        }

        public async Task RefreshAsync(string key, CancellationToken token = default)
        {
            await _inner.RefreshAsync(key, token);
        }

        public void Remove(string key)
        {
            _inner.Remove(key);
        }

        public async Task RemoveAsync(string key, CancellationToken token = default)
        {
            await _inner.RemoveAsync(key, token);
        }

        private void TrackAccess(string prefixedKey)
        {
            _db.SortedSetIncrement(
               "cache_access",
               prefixedKey,
               1.0);

            // Устанавливаем базовый TTL при первом обращении
            var currentTtl = _db.KeyTimeToLive(prefixedKey);
            if (!currentTtl.HasValue)
            {
                _db.KeyExpire(
                    prefixedKey,
                    TimeSpan.FromMinutes(10),
                    CommandFlags.FireAndForget);
            }
        }

        private async Task TrackAccessAsync(string prefixedKey)
        {
            await _db.SortedSetIncrementAsync(
                "cache_access",
                prefixedKey,
                1.0);

            // Устанавливаем базовый TTL при первом обращении
            var currentTtl = await _db.KeyTimeToLiveAsync(prefixedKey);
            if (!currentTtl.HasValue)
            {
                await _db.KeyExpireAsync(
                    prefixedKey,
                    TimeSpan.FromMinutes(10),
                    CommandFlags.FireAndForget);
            }
        }
    }
}