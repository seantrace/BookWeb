using BookWeb.Application.Interfaces.Services;
using BookWeb.Server.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookWeb.Server.Controllers.CalibreAPI
{
    [Route("api/[controller]")]
    [ApiController]
#if !DEBUG
    [Authorize]
#endif
    [ApiExplorerSettings(GroupName = "v2")]
    public class LibrariesController : ControllerBase
    {

        private readonly ICurrentUserService _currentUserService;
        private readonly ILibraryService _libraryService;

        public LibrariesController(ICurrentUserService currentUserService, ILibraryService libraryService)
        {
            _currentUserService = currentUserService;
            _libraryService = libraryService;
        }

        // GET: api/<LibraryController>
        [HttpGet]
        public IEnumerable<Library> Get()
        {
            return _libraryService.Libraries;
        }

        // GET api/<LibraryController>/5
        [HttpGet("{id}")]
        public Library Get(int id)
        {
            return new Library()
            {
                Id = 1,
                Name = "Fiction",
                Path = @"F:\EBooks\Libraries\Fiction",
                BookCount = 803,
                UserAccess = new List<long>() { 1 }
            };
        }

        // POST api/<LibraryController>
        [HttpPost]
        public async Task<ActionResult<Library>> Post([FromBody] Library library)
        {
            Library newlib = await _libraryService.AddLibrary(library);
            return Ok(newlib);
        }

        // PUT api/<LibraryController>/5
        [HttpPut("")]
        public async Task<ActionResult<Library>> Put( [FromBody] Library library)
        {
            Library newlib = await _libraryService.UpdateLibrary(library);
            if (newlib == null)
                return NoContent();
            else
                return Ok(newlib);
        }

        // DELETE api/<LibraryController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            bool result = await _libraryService.DeleteLibrary(id);
            if (!result)
            {
                return NoContent();
            } else
            {
                return Ok();
            }
        }
    }
}
