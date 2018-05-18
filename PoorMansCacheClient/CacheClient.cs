using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;

namespace PoorMansCacheClient {
    public class CacheClient {
        readonly HttpClient httpClient;
        readonly IMemoryCache memoryCache;

        public CacheClient(string baseUrl, IMemoryCache memoryCache) {
            var url = $"{baseUrl}/api/Cache/";
            httpClient = new HttpClient { BaseAddress = new Uri(url) };

            this.memoryCache = memoryCache;
        }

        public async Task<(bool exists, T value)> TryGetValue<T>(string key) {
            if (!memoryCache.TryGetValue(key, out T cacheResult)) {
                var response = await httpClient.GetAsync(key);
                if (!response.IsSuccessStatusCode || response.StatusCode == HttpStatusCode.NoContent) {
                    return (false, default(T));
                }

                var result = await response.Content.ReadAsStringAsync();
                cacheResult = JsonConvert.DeserializeObject<T>(result);
            }

            return (true, cacheResult);
        }

        public async Task Set<T>(string key, T value) {
            memoryCache.Set(key, value);
            var serializedValue = JsonConvert.SerializeObject(value);
            var content = new StringContent(serializedValue, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(key, content);
            response.EnsureSuccessStatusCode();
        }

		public async Task<T> GetOrSet<T>(string key, Func<Task<T>> valueFactory) {
			var result = await TryGetValue<T>(key);
			if (result.exists) {
				return result.value;
			}

			var value = await valueFactory();
			await Set(key, value);         
			return value;
		}
    }
}
