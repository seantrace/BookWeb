using BookWeb.Client.Shared;
using BookWeb.Shared.BookWebModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace BookWeb.Client.Services
{
    public class CalibreService : ICalibreService
    {
        private readonly HttpClient httpClient;
        public CalibreService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        #region Authors
        public async Task<List<Author>> GetAuthors()
        {
            return await ApiHelpers.MakeRequestAsync<object, List<Author>>(httpClient, "GET", "api/Authors");
        }

        public async Task<Author> GetAuthor(int id)
        {
            return await ApiHelpers.MakeRequestAsync<object, Author>(httpClient, "GET", $"api/Authors/{id}");
        }

        public async Task<List<ImageData>> GetAuthorImageUrls(string name)
        {
            return await ApiHelpers.MakeRequestAsync<object, List<ImageData>>(httpClient, "GET", $"api/Authors/ImageUrls/{name}");
        }

        public async Task<AuthorData> GetAuthorData(string name)
        {
            return await ApiHelpers.MakeRequestAsync<object, AuthorData>(httpClient, "GET", $"api/Authors/Data/{name}");
        }
        #endregion

        #region Books


        public async Task<Book> GetBook(int id)
        {
            return await ApiHelpers.MakeRequestAsync<object, Book>(httpClient, "GET", $"api/Books/{id}");
        }

        public async Task<GetBookListResponseDto> GetBooks(int pagesize, int pageno)
        {
            string url = $"api/Books?Limit={pagesize}&Page={pageno}";
            return await ApiHelpers.MakeRequestAsync<object, GetBookListResponseDto>(httpClient, "GET", url);
        }

        public async Task<List<Book>> GetBooksByAuthor(long id)
        {
            string url = $"api/Books/ByAuthor/{id}";
            return await ApiHelpers.MakeRequestAsync<object, List<Book>>(httpClient, "GET", url);
        }

        public async Task<List<Book>> GetBooksBySeries(long id)
        {
            string url = $"api/Books/BySeries/{id}";
            return await ApiHelpers.MakeRequestAsync<object, List<Book>>(httpClient, "GET", url);
        }

        public async Task<List<Book>> GetBooksByTag(long id)
        {
            string url = $"api/Books/ByTag/{id}";
            return await ApiHelpers.MakeRequestAsync<object, List<Book>>(httpClient, "GET", url);
        }
        public async Task<List<Book>> GetRecentBooks(int number)
        {
            string url = $"api/Books/Recent/{number}";
            return await ApiHelpers.MakeRequestAsync<object, List<Book>>(httpClient, "GET", url);
        }

        public async Task<List<Book>> GetBooksByPublisher(long id)
        {
            string url = $"api/Books/ByPublisher/{id}";
            return await ApiHelpers.MakeRequestAsync<object, List<Book>>(httpClient, "GET", url);

        }

        public async Task<List<Book>> GetBooksByLanguage(long id)
        {
            string url = $"api/Books/ByLanguage/{id}";
            return await ApiHelpers.MakeRequestAsync<object, List<Book>>(httpClient, "GET", url);

        }

        #endregion
        #region Languages
        public async Task<List<Language>> GetLanguages()
        {
            return await ApiHelpers.MakeRequestAsync<object, List<Language>>(httpClient, "GET", "Languages");
        }

        public async Task<Language> GetLanguage(long id)
        {
            string url = $"api/Languages/{id}";
            return await ApiHelpers.MakeRequestAsync<object, Language>(httpClient, "GET", url);
        }
        #endregion
        #region Publishers

        public async Task<List<Publisher>> GetPublishers()
        {
            return await ApiHelpers.MakeRequestAsync<object, List<Publisher>>(httpClient, "GET", "api/Publishers");
        }

        public async Task<Publisher> GetPublisher(long id)
        {
            string url = $"api/Publishers/{id}";
            return await ApiHelpers.MakeRequestAsync<object, Publisher>(httpClient, "GET", url);
        }
        #endregion
        #region Series

        public async Task<List<Series>> GetSeries()
        {
            return await ApiHelpers.MakeRequestAsync<object, List<Series>>(httpClient, "GET", "api/Series");
        }

        public async Task<Series> GetSeries(long id)
        {
            string url = $"api/Series/{id}";
            return await ApiHelpers.MakeRequestAsync<object, Series>(httpClient, "GET", url);
        }

        public async Task<SeriesData> GetSeriesData(string name)
        {
            string url = $"api/Series/Data/{name}";
            return await ApiHelpers.MakeRequestAsync<object, SeriesData>(httpClient, "GET", url);
        }
        #endregion
        #region Tags

        public async Task<List<Tag>> GetTags()
        {
            return await ApiHelpers.MakeRequestAsync<object, List<Tag>>(httpClient, "GET", "api/Tags");
        }

        public async Task<Tag> GetTag(long id)
        {
            string url = $"api/Tags/{id}";
            return await ApiHelpers.MakeRequestAsync<object, Tag>(httpClient, "GET", url);

        }
        #endregion

        #region Data
        public async Task<List<Datum>> GetData()
        {
            return await ApiHelpers.MakeRequestAsync<object, List<Datum>>(httpClient, "GET", "api/Data");
        }
        #endregion

        #region Format
        public async Task<List<Book>> GetBooksByFormat(long id)
        {
            List<Book> Books = new List<Book>();
            List<Format> Formats = await GetFormats();
            Format format = Formats.First(item => item.Id == id);
            List<Datum> Data = await GetData();
            foreach (Datum d in Data.Where(item => item.Format  == format.Name))
            {
                Book book = new Book();
                book.Id = d.Book;
                book.Title = d.Name;
                Books.Add(book);
            };
            return Books;
        }

        public async Task<List<Format>> GetFormats()
        {
            List<Datum> data = await GetData();
            int IdNo = 1;
            List<Datum> unique = data.GroupBy(item => item.Format).Select(item => item.First()).ToList();
            List<Format> formats = unique.Select(item => new Format
            {
                Name = item.Format,
                Id = IdNo++,
            }).ToList();

            return formats;
        }

        public async Task<Format> GetFormat(long id)
        {
            List<Format> formats = await GetFormats();
            return formats.First(item => item.Id == id);
        }
        #endregion

        #region Libraries

        public async Task<List<Library>> GetLibraries()
        {
            return await ApiHelpers.MakeRequestAsync<object, List<Library>>(httpClient, "GET", "api/Libraries");
        }

        public async Task<Library> GetLibrary(long id)
        {
            string url = $"api/Libraries/{id}";
            return await ApiHelpers.MakeRequestAsync<object, Library>(httpClient, "GET", url);
        }

 
        #endregion
    }
}
