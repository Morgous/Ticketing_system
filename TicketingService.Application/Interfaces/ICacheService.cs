using Microsoft.Extensions.Caching.Distributed;

namespace TicketingService.Application.Interfaces
{
    public interface ICacheService
    {
        Task<string> GetStringAsync(string key);
        Task SetStringAsync(string key, string value, DistributedCacheEntryOptions options);
        Task RemoveAsync(string key);
    }
}
