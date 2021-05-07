using BookWeb.Shared.BookWebModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookWeb.Client.Services
{
    public interface ICalibreService
    {
        Task<Book> GetBook(int id);

        Task<GetBookListResponseDto> GetBooks(int pagesize, int pageno);

        Task<List<Book>> GetRecentBooks(int number);

        Task<List<Book>> GetBooksByAuthor(long id);

        Task<List<Book>> GetBooksBySeries(long id);

        Task<List<Book>> GetBooksByTag(long id);

        Task<List<Book>> GetBooksByPublisher(long id);

        Task<List<Book>> GetBooksByLanguage(long id);

        Task<List<Author>> GetAuthors();

        Task<Author> GetAuthor(int id);

        Task<List<Series>> GetSeries();

        Task<Series> GetSeries(long id);

        Task<List<Tag>> GetTags();

        Task<Tag> GetTag(long id);

        Task<List<Publisher>> GetPublishers();

        Task<Publisher> GetPublisher(long id);

        Task<List<Language>> GetLanguages();

        Task<Language> GetLanguage(long id);

        Task<List<Datum>> GetData();

        Task<List<Format>> GetFormats();

        Task<Format>  GetFormat(long id);

        Task<List<Book>> GetBooksByFormat(long id);

        Task<List<Library>> GetLibraries();

        Task<Library> GetLibrary(long id);

        Task<List<ImageData>> GetAuthorImageUrls(string name);

        Task<AuthorData> GetAuthorData(string name);

        Task<SeriesData> GetSeriesData(string name);
    }
}
