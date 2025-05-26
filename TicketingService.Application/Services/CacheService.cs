using Microsoft.Extensions.Caching.Distributed;
using TicketingService.Application.Interfaces;

namespace TicketingService.Application.Services
{
    public class CacheService : ICacheService
    {
        private readonly IDistributedCache _cache;

        public CacheService(IDistributedCache cache)
        {
            _cache = cache;
        }

        public async Task<string> GetStringAsync(string key)
        {
            return await _cache.GetStringAsync(key);
        }

        public async Task SetStringAsync(string key, string value, DistributedCacheEntryOptions options)
        {
            await _cache.SetStringAsync(key, value, options);
        }

        public async Task RemoveAsync(string key)
        {
            await _cache.RemoveAsync(key);
        }
    }
}
