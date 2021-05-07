using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookWeb
{
    [Route("api/[controller]")]
    [ApiController]
#if !DEBUG
    [Authorize]
#endif
    [ResponseCache(Duration = 300, Location = ResponseCacheLocation.Any, NoStore = false)]
    public class DataController : ControllerBase
    {
        private readonly CalibreDBContext _context;

        public DataController(CalibreDBContext context)
        {
            _context = context;
        }

        // GET: api/Data
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Datum>>> GetData()
        {
            return await _context.Data.ToListAsync();
        }

        // GET: api/Data/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Datum>> GetDatum(long id)
        {
            var datum = await _context.Data.FindAsync(id);

            if (datum == null)
            {
                return NotFound();
            }

            return datum;
        }

 
        private bool DatumExists(long id)
        {
            return _context.Data.Any(e => e.Id == id);
        }
    }
}
