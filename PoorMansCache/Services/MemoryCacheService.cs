using Microsoft.Extensions.Caching.Memory;
using PoorMansCache.Model;

namespace PoorMansCache.Services
{
    public class MemoryCacheService
    {
        readonly IMemoryCache memoryCache;

        public MemoryCacheService(IMemoryCache memoryCache)
        {
            this.memoryCache = memoryCache;
            // TODO: Invalidation
        }

        public CacheResult Get(string key) {
            if (memoryCache.TryGetValue(key, out object value)) {
                return new CacheResult(true, value);
            }

            return new CacheResult(false, value);
        }

        public void Set(string key, object value) =>
            memoryCache.Set(key, value);
    }
}
