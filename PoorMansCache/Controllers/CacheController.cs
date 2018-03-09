using Microsoft.AspNetCore.Mvc;
using PoorMansCache.Model;
using PoorMansCache.Services;

namespace PoorMansCache.Controllers
{
    [Route("api/[controller]")]
    public class CacheController : Controller
    {
        readonly MemoryCacheService memoryCacheService;

        public CacheController(MemoryCacheService memoryCacheService)
        {
            this.memoryCacheService = memoryCacheService;
        }

        [HttpGet("{key}")]
        public IActionResult Get(string key)
        {
            // TODO: Error handling
            var cacheResult = memoryCacheService.Get(key);
            if (cacheResult.Exist) {
                return new JsonResult(cacheResult.Value);
            }

            return NoContent();
        }

        [HttpPost("{key}")]
        public void Post(string key, [FromBody]object value) {
            // TODO: Error handling
            memoryCacheService.Set(key, value);
        }
    }
}
