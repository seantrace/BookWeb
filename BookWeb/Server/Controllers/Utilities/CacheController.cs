using LazyCache;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookWeb.Server.Controllers.Utilities
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "v2")]
#if !DEBUG
    [Authorize]
#endif
    public class CacheController : ControllerBase
    {
        private readonly IAppCache _cache;

        public CacheController(IAppCache cache)
        {
            _cache = cache;
        }

        // DELETE api/<CacheController>
        [HttpDelete("")]
        public ActionResult<long> Delete()
        {
            long version = _cache.IncrementVersion();
            return version;
        }
    }
}
