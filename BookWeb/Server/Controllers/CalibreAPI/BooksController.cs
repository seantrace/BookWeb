using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading;
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
    public class BooksController : ControllerBase
    {
        private readonly CalibreDBContext _context;
        private readonly IAppCache _cache;

        public record UrlQueryParameters(int Limit = 50, int Page = 1);

        public BooksController(CalibreDBContext context, IAppCache cache)
        {
            _context = context;
            _cache = cache;
        }

        // GET: api/Books
        [HttpGet]
        [ProducesResponseType(typeof(GetBookListResponseDto), 200)]
        [ProducesResponseType(typeof(ProblemDetails), 400)]
        public async Task<ActionResult<GetBookListResponseDto>> GetBooks([FromQuery] UrlQueryParameters urlQueryParameters,
            CancellationToken cancellationToken)
        {
            Stopwatch w = new Stopwatch();
            w.Start();
            Func<Task<ActionResult<GetBookListResponseDto>>> booksGetter = async () => {

                var books = await _context.Books
                    .AsNoTracking()
                    .PaginateAsync(urlQueryParameters.Page, urlQueryParameters.Limit, cancellationToken);

                foreach (var book in books.Items)
                {
                    var seriesLink = _context.BooksSeriesLinks.AsNoTracking().Where<BooksSeriesLink>(link => link.Book == book.Id);
                    if (seriesLink.Count() > 0)
                    {
                        book.SeriesId = seriesLink.First<BooksSeriesLink>().Series;
                        var series = await _context.Series.FindAsync(book.SeriesId);
                        book.Series = series.Name;
                    }

                    var authorLink = _context.BooksAuthorsLinks.AsNoTracking().Where<BooksAuthorsLink>(link => link.Book == book.Id);
                    if (authorLink.Count() > 0)
                    {
                        book.AuthorId = authorLink.First<BooksAuthorsLink>().Author;
                        var author = await _context.Authors.FindAsync(book.AuthorId);
                        book.Author = author.Name;
                    }
                }

                var result = new GetBookListResponseDto
                {
                    PageSize = books.PageSize,
                    CurrentPage = books.CurrentPage,
                    TotalPages = books.TotalPages,
                    TotalItems = books.TotalItems,
                    FetchTime = w.ElapsedMilliseconds,
                    Items = (List<Book>)books.Items
                };

                return result;
            };

            var booksWithCaching = await _cache.GetOrAddAsync($"BooksController.Get-{urlQueryParameters.Limit}-{urlQueryParameters.Page}", booksGetter);
            w.Stop();

            return booksWithCaching;
        }


        // GET: api/Books/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBook(long id)
        {
            var book = await _context.Books.FindAsync(id);

            if (book == null)
            {
                return NotFound();
            }

            await extendBook(book);
            return book;
        }

        // GET: api/Books/5/Thumbnail
        [HttpGet("{id}/Thumbnail")]
        [ResponseCache(Duration = 1000000, Location = ResponseCacheLocation.Any, NoStore = false)]
        public async Task<FileStreamResult> GetBookThumbnail(long id)
        {
            var book = await _context.Books.FindAsync(id);
            MemoryStream stream = new MemoryStream();

            if (book == null)
            {
                return new FileStreamResult(stream, "image/png");
            }

            if (System.IO.File.Exists($"F:\\EBooks\\Libraries\\Fiction\\.cache\\b{book.Id}.png"))
            {
                Image original = Bitmap.FromFile($"F:\\EBooks\\Libraries\\Fiction\\.cache\\b{book.Id}.png");
                original.Save(stream, ImageFormat.Png);
                stream.Position = 0;
                return File(stream, "image/png");
            }
            else
            {
                Image original = Bitmap.FromFile($"F:\\EBooks\\Libraries\\Fiction\\{book.Path}\\cover.jpg");
                var thumbnail = Utilities.ResizeImage(original, 150, 230);
                thumbnail.Save($"F:\\EBooks\\Libraries\\Fiction\\.cache\\b{book.Id}.png", ImageFormat.Png);
                thumbnail.Save(stream, ImageFormat.Png);
                stream.Position = 0;
                return File(stream, "image/png");
            }
        }

        // GET: api/Books/5/Download
        [HttpGet("{id}/Download")]
        public async Task<FileStreamResult> GetBookDownload(long id)
        {
            var book = await _context.Books.FindAsync(id);
            MemoryStream stream = new MemoryStream();

            //if (book == null)
            //{
            //    return new FileStreamResult(stream, "image/png");
            //}

            //var data = _context.Data.AsNoTracking().Where<Datum>(datum => datum.Book == book.Id);
            //if (data.Count() > 0)
            //{
            //    Datum datum = data.First<Datum>();
            //    book.Format = datum.Format;
            //    book.Size = datum.UncompressedSize;
            //    book.Filename = datum.Name + $".{datum.Format.ToLower()}";
            //}

            //System.IO.File.OpenRead($"F:\\EBooks\\Libraries\\Fiction\\{book.Path}\\{book.Filename}").CopyTo(stream);
            //stream.Position = 0;

            return File(stream, "application/epub+zip");
        }

        // GET: api/Books/Recent
        [HttpGet("Recent/{number}")]
        public async Task<ActionResult<List<Book>>> GetRecentBooks(int number)
        {
            var num = await _context.Books.CountAsync();
            List<Book> books = new List<Book>();
            books = _context.Books.Skip<Book>(Math.Max(0, num - number)).ToList<Book>();
            books.Reverse();
            foreach (var book in books)
            {
                await extendBook(book);
            }
            return books;
        }

        // GET: api/Books/ByAuthor
        [HttpGet("ByAuthor/{id}")]
        public async Task<ActionResult<List<Book>>> GetBooksByAuthor(long id)
        {
            var links = _context.BooksAuthorsLinks.Where<BooksAuthorsLink>(link => link.Author == id);
            List<Book> books = new List<Book>();
            foreach (var link in links)
            {
                var result = await GetBook(link.Book);
                books.Add(result.Value);
            }
            return books;
        }

        // GET: api/Books/ByTag
        [HttpGet("ByTag/{id}")]
        public async Task<ActionResult<List<Book>>> GetBooksByTag(long id)
        {
            var links = _context.BooksTagsLinks.Where<BooksTagsLink>(link => link.Tag == id);
            List<Book> books = new List<Book>();
            foreach (var link in links)
            {
                var result = await GetBook(link.Book);
                books.Add(result.Value);
            }
            return books;
        }

        // GET: api/Books/ByLanguage
        [HttpGet("ByLanguage/{id}")]
        public async Task<ActionResult<List<Book>>> GetBooksByByLanguage(long id)
        {
            var links = _context.BooksLanguagesLinks.Where(link => link.LangCode == id);
            List<Book> books = new List<Book>();
            foreach (var link in links)
            {
                var result = await GetBook(link.Book);
                books.Add(result.Value);
            }
            return books;
        }

        // GET: api/Books/ByPublisher
        [HttpGet("ByPublisher/{id}")]
        public async Task<ActionResult<List<Book>>> GetBooksByPublisher(long id)
        {
            var links = _context.BooksPublishersLinks.Where(link => link.Publisher == id);
            List<Book> books = new List<Book>();
            foreach (var link in links)
            {
                var result = await GetBook(link.Book);
                books.Add(result.Value);
            }
            return books;
        }

        // GET: api/Books/BySeries
        [HttpGet("BySeries/{id}")]
        public async Task<ActionResult<List<Book>>> GetBooksBySeries(long id)
        {
            var links = _context.BooksSeriesLinks.Where(link => link.Series == id);
            List<Book> books = new List<Book>();
            foreach (var link in links)
            {
                var result = await GetBook(link.Book);
                books.Add(result.Value);
            }
            return books;
        }

        private bool BookExists(long id)
        {
            return _context.Books.Any(e => e.Id == id);
        }

        private async Task extendBook(Book book)
        {
            var comment = _context.Comments.AsNoTracking().Where<Comment>(comment => comment.Book == book.Id);
            if (comment.Count() > 0)
            {
                book.Comment = comment.First<Comment>().Text;
            }

            var identifiers = await _context.Identifiers.AsNoTracking().Where(identifier => identifier.Book == book.Id).ToListAsync<Identifier>();
            book.Identifiers = identifiers;

            var data = _context.Data.AsNoTracking().Where<Datum>(datum => datum.Book == book.Id);
            if (data.Count() > 0)
            {
                List<BookFormat> formats = new List<BookFormat>();
                book.Formats = formats;
                foreach (Datum datum in data.ToList())
                {
                    BookFormat format = new BookFormat();
                    format.Name = datum.Format;
                    format.Size = datum.UncompressedSize;
                    format.Filename = datum.Name + $".{datum.Format.ToLower()}";
                    formats.Add(format);
                }
            }

            var link = _context.BooksLanguagesLinks.AsNoTracking().Where<BooksLanguagesLink>(link => link.Book == book.Id);
            if (link.Count() > 0)
            {
                var langCode = link.First<BooksLanguagesLink>().LangCode;
                var language = await _context.Languages.FindAsync(langCode);
                book.Language = Iso639.Language.FromPart3(language.LangCode).Name;
            }

            var seriesLink = _context.BooksSeriesLinks.AsNoTracking().Where<BooksSeriesLink>(link => link.Book == book.Id);
            if (seriesLink.Count() > 0)
            {
                book.SeriesId = seriesLink.First<BooksSeriesLink>().Series;
                var series = await _context.Series.FindAsync(book.SeriesId);
                book.Series = series.Name;
            }

            var authorLink = _context.BooksAuthorsLinks.AsNoTracking().Where<BooksAuthorsLink>(link => link.Book == book.Id);
            if (authorLink.Count() > 0)
            {
                book.AuthorId = authorLink.First<BooksAuthorsLink>().Author;
                var author = await _context.Authors.FindAsync(book.AuthorId);
                book.Author = author.Name;
            }

            var publisherLink = _context.BooksPublishersLinks.AsNoTracking().Where<BooksPublishersLink>(link => link.Book == book.Id);
            if (publisherLink.Count() > 0)
            {
                book.PublisherId = publisherLink.First<BooksPublishersLink>().Publisher;
                var publisher = await _context.Publishers.FindAsync(book.PublisherId);
                book.Publisher = publisher.Name;
            }

            var tagLinks = await _context.BooksTagsLinks.AsNoTracking().Where(tag => tag.Book == book.Id).ToListAsync<BooksTagsLink>();
            if (tagLinks.Count() > 0)
            {
                book.Tags = new List<Tag>();
                foreach (var tagLink in tagLinks)
                {
                    Tag tag = await _context.Tags.FindAsync(tagLink.Tag);
                    book.Tags.Add(tag);
                }
            }

            var ratingLinks = await _context.BooksRatingsLinks.AsNoTracking().Where(tag => tag.Book == book.Id).ToListAsync<BooksRatingsLink>();
            if (ratingLinks.Count() > 0)
            {
                var rating = await _context.Ratings.AsNoTracking().Where(rating => rating.Id == ratingLinks[0].Rating).ToListAsync<Rating>();
                book.Rating = (long)rating[0].RatingValue;
            }
        }
    }
}
