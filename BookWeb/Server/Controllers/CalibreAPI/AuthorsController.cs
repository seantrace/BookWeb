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
    public class AuthorsController : ControllerBase
    {
        private readonly CalibreDBContext _context;
        //private readonly WebScraper _scraper;

        public AuthorsController(CalibreDBContext context)
        {
            _context = context;
            //_scraper = scraper;
        }

        // GET: api/Authors
        [HttpGet]
        public async Task<ActionResult<List<Author>>> GetAuthors()
        {
            var authors = await _context.Authors.ToListAsync();
            return authors;
        }

        // GET: api/Authors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Author>> GetAuthor(long id)
        {
            var author = await _context.Authors.FindAsync(id);

            if (author == null)
            {
                return NotFound();
            }

            return author;
        }

        //
        // GET: api/Authors/ImageUrls/5
        [HttpGet("ImageUrls/{name}")]
        public async Task<ActionResult<List<ImageData>>> GetAuthorImageUrls([FromServices] IWebScraper scraper, string name)
        {
            List<ImageData> imageData;
            try
            {
                imageData = await scraper.GetAuthorImageUrlsAsync(name);
            }
            catch
            {
                return NotFound();
            }

            return imageData;
        }

        // GET: api/Authors/Data/Lee+Child
        [HttpGet("Data/{name}")]
        public async Task<ActionResult<AuthorData>> GetAuthorData([FromServices] IWebScraper scraper, string name)
        {
            AuthorData authorData;
            authorData = await scraper.GetAuthorDataAsync(name);
            return authorData;
        }

        private bool AuthorExists(long id)
        {
            return _context.Authors.Any(e => e.Id == id);
        }
    }
}
