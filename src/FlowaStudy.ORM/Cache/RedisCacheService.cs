using FlowaStudy.Application.Cache;
using StackExchange.Redis;
using System.Globalization;
using System.Text.Json;

namespace FlowaStudy.ORM.Cache
{
    public class RedisCacheService : ICacheService
    {
        private readonly IDatabase _db;

        public RedisCacheService(IConnectionMultiplexer redis)
        {
            _db = redis.GetDatabase();
        }

        public async Task SetAsync<T>(string key, T value, TimeSpan? expiry = null)
        {
            var json = JsonSerializer.Serialize(value);
            await _db.StringSetAsync(key, json, expiry);
        }

        public async Task<T?> GetAsync<T>(string key)
        {
            var value = await _db.StringGetAsync(key);
            return value.HasValue ? JsonSerializer.Deserialize<T>(value!) : default;
        }

        public async Task RemoveAsync(string key)
        {
            await _db.KeyDeleteAsync(key);
        }

        public async Task SetPriceAsync( Guid id, decimal price, TimeSpan? expiry = null)
        {
            var key = $"asset:{id}:price";
            await _db.StringSetAsync(key, price.ToString(CultureInfo.InvariantCulture), expiry ?? TimeSpan.FromSeconds(60));
        }

        public async Task<decimal?> GetPriceAsync(Guid id)
        {
            var key = $"asset:{id}:price";
            var value = await _db.StringGetAsync(key);
            if (value.IsNullOrEmpty) return null;

            if (decimal.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out var price))
                return price;

            return null;
        }
    }
}
