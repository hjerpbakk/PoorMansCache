using Microsoft.AspNetCore.Mvc;
using PoorMansCache.Services;

namespace PoorMansCache.Controllers
{
    [Route("/")]
    public class VersionController : Controller
    {
        readonly VersionService versionService;

        public VersionController(VersionService versionService)
        {
            this.versionService = versionService;
        }

        [HttpGet]
        public string Get() => versionService.GetCurrentVersion();
    }
}
