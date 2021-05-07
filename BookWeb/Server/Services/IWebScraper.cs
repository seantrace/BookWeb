using BookWeb.Shared.BookWebModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookWeb.Server.Services
{
    public interface IWebScraper
    {
        public Task<List<ImageData>> GetAuthorImageUrlsAsync(string name, int maximages = 20);

        public Task<AuthorData> GetAuthorDataAsync(string name, int maximages = 20);

        public Task<SeriesData> GetSeriesDataAsync(string name);
    }
}
