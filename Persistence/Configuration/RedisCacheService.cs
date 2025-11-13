using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Configuration
{
    public class RedisCacheService
    {
        private readonly IDatabase _db;

        public RedisCacheService(IConnectionMultiplexer redis)
        {
            _db = redis.GetDatabase();
        }

        public async Task SetAsync(string key, string value, TimeSpan? expiry = null)
            => await _db.StringSetAsync(key, value, expiry);

        public async Task<string?> GetAsync(string key)
            => await _db.StringGetAsync(key);

        public async Task RemoveAsync(string key)
            => await _db.KeyDeleteAsync(key);
    }

}
