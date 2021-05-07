using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookWeb.Server.Services;
using BookWeb.Shared.BookWebModels;
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
    public class SeriesController : ControllerBase
    {
        private readonly CalibreDBContext _context;

        public SeriesController(CalibreDBContext context)
        {
            _context = context;
        }

        // GET: api/Series
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Series>>> GetSeries()
        {
            return await _context.Series.ToListAsync();
        }

        // GET: api/Series/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Series>> GetSeries(long id)
        {
            var series = await _context.Series.FindAsync(id);

            if (series == null)
            {
                return NotFound();
            }

            return series;
        }

        // GET: api/Series/Data/Discworld
        [HttpGet("Data/{name}")]
        public async Task<ActionResult<SeriesData>> GetSeriesData([FromServices] IWebScraper scraper, string name)
        {
            SeriesData seriesData = await scraper.GetSeriesDataAsync(name);
            return seriesData;
        }

        private bool SeriesExists(long id)
        {
            return _context.Series.Any(e => e.Id == id);
        }
    }
}
