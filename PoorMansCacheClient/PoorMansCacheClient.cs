using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PoorMansCacheClient
{
    // TODO: Endre navn
    public class PoorMansCacheClient
    {
        readonly HttpClient httpClient;

        public PoorMansCacheClient(string baseUrl)
        {
            var url = $"{baseUrl}/api/Cache/";
            httpClient = new HttpClient
            {
                BaseAddress = new Uri(url)
            };
        }

        public async Task<(bool exists, T value)> TryGetValue<T>(string key)
        {
            var response = await httpClient.GetAsync(key);
            if (!response.IsSuccessStatusCode || response.StatusCode == HttpStatusCode.NoContent) {
                return (false, default(T));
            }

            var result = await response.Content.ReadAsStringAsync();
            var cacheResult = JsonConvert.DeserializeObject<T>(result);
            return (true, cacheResult);
        }

        public async Task Set<T>(string key, T value)
        {
            var serializedValue = JsonConvert.SerializeObject(value);
            var content = new StringContent(serializedValue, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(key, content);
            response.EnsureSuccessStatusCode();
        }
    }
}
