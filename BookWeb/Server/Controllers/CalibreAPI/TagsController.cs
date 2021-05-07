using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using LazyCache;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookWeb
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "v2")]
#if !DEBUG
    [Authorize]
#endif
    [ResponseCache(Duration = 300, Location = ResponseCacheLocation.Any, NoStore = false)]
    public class TagsController : ControllerBase
    {
        private readonly CalibreDBContext _context;
        private readonly IAppCache _cache;

        public TagsController(CalibreDBContext context, IAppCache cache)
        {
            _context = context;
            _cache = cache;
        }

        // GET: api/Tags
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tag>>> GetTags()
        {
            Stopwatch w = new Stopwatch();
            w.Start();
            Func<Task<ActionResult<IEnumerable<Tag>>>> tagGetter = async () => await _context.Tags.ToListAsync();

            var tagsWithCaching = await _cache.GetOrAddAsync("TagController.Get", tagGetter);

            w.Stop();
            return tagsWithCaching;
        }

        // GET: api/Tags/5
        [HttpGet("{id}")]

        public async Task<ActionResult<Tag>> GetTag(long id)
        {
            Stopwatch w = new Stopwatch();
            w.Start();
            Func<Task<ActionResult<Tag>>> tagGetter = async () =>  await _context.Tags.FindAsync(id);
            var tagWithCaching = await _cache.GetOrAddAsync($"TagController.Get-{id}", tagGetter);

            //var tag = await _context.Tags.FindAsync(id);

            //if (tag == null)
            //{
            //    return NotFound();
            //}

            w.Stop();

            if (tagWithCaching.Value == null)
            {
                return NotFound();
            }
            return tagWithCaching.Value;
        }

        private bool TagExists(long id)
        {
            return _context.Tags.Any(e => e.Id == id);
        }
    }
}
