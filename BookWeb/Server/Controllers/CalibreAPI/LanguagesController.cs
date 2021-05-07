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
    [ApiExplorerSettings(GroupName = "v2")]
#if !DEBUG
    [Authorize]
#endif
    [ResponseCache(Duration = 300, Location = ResponseCacheLocation.Any, NoStore = false)]
    public class LanguagesController : ControllerBase
    {
        private readonly CalibreDBContext _context;

        public LanguagesController(CalibreDBContext context)
        {
            _context = context;
        }

        // GET: api/Languages
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Language>>> GetLanguages()
        {
            List<Language> list = await _context.Languages.ToListAsync();
            foreach (Language language in list)
            {
                language.Name = Iso639.Language.FromPart3(language.LangCode).Name;
            }
            return list;
        }

        // GET: api/Languages/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Language>> GetLanguage(long id)
        {
            var language = await _context.Languages.FindAsync(id);
            language.Name = Iso639.Language.FromPart3(language.LangCode).Name;
            if (language == null)
            {
                return NotFound();
            }

            return language;
        }

        private bool LanguageExists(long id)
        {
            return _context.Languages.Any(e => e.Id == id);
        }
    }
}
