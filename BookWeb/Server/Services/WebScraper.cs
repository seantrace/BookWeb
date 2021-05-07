using BookWeb.Shared.BookWebModels;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;

namespace BookWeb.Server.Services
{
    public class WebScraper : IWebScraper
    {
        const string desktopAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/90.0.4430.93 Safari/537.36";
        const string mobileAgent = "Mozilla/5.0 (Linux; Android 8.0; Pixel 2 Build/OPD3.170816.012) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/90.0.4430.93 Mobile Safari/537.36";
        
        const string yandexBaseUrl = "https://yandex.com/";
        const string goodreadsBaseUrl = "https://www.goodreads.com/";
        const string bookseriesinorderBaseUrl = "https://www.bookseriesinorder.com/";

        public async Task<List<ImageData>> GetAuthorImageUrlsAsync(string name, int maximages)
        {
            List<ImageData> imageData = new List<ImageData>();

            string authorSearch = ("author " + name).Replace(' ', '+').ToLower();
            var web = new HtmlWeb();
            web.UserAgent = desktopAgent;
            try
            {
                var doc = await web.LoadFromWebAsync($"{yandexBaseUrl}images/search?text={authorSearch}&type=face&iorient=square");
                var imagenodes = doc.DocumentNode.SelectNodes($"//img[contains(@class, 'serp-item__thumb')]");
                foreach (var node in imagenodes.Take(maximages))
                {
                    ImageData data = new ImageData();
                    data.Url = HttpUtility.HtmlDecode("https:" + node.Attributes["src"].Value);
                    (Bitmap b, string encoded) = bitmapFromUrl(data.Url);
                    data.Width = b.Width; data.Height = b.Height;
                    data.AspectRatio = (float)b.Width / (float)b.Height;
                    data.Data = encoded;
                    imageData.Add(data);
                }
            }

            catch
            {
                throw new WebScrapeException(string.Format("Unable to find images for ", name));
            }

            return imageData;
        }


        public async Task<AuthorData> GetAuthorDataAsync(string name, int maximages)
        {
            AuthorData authorData = new AuthorData();
            authorData.Name = name;

            List<ImageData> imageDataList = new List<ImageData>();
            List<AuthorPageData> pageDataList = new List<AuthorPageData>();

            string authorSearch = name.Replace(' ', '+').ToLower();
            var web = new HtmlWeb();
            web.UserAgent = desktopAgent;
            try
            {
                // Search Yandex for Images
                var docy = await web.LoadFromWebAsync($"{yandexBaseUrl}images/search?text={authorSearch}&type=face&iorient=square");
                var imagenodes = docy.DocumentNode.SelectNodes($"//img[contains(@class, 'serp-item__thumb')]");
                foreach (var node in imagenodes.Take(maximages))
                {
                    ImageData data = new ImageData();
                    data.Url = HttpUtility.HtmlDecode("https:" + node.Attributes["src"].Value);
                    (Bitmap b, string encoded) = bitmapFromUrl(data.Url);
                    data.Width = b.Width; data.Height = b.Height;
                    data.AspectRatio = (float)b.Width / (float)b.Height;
                    data.Data = encoded;
                    imageDataList.Add(data);
                }

                // Search GoodReads for author page
                AuthorPageData page = new AuthorPageData();
                page.SiteName = "goodreads";
                page.Url = $"{goodreadsBaseUrl}book/author/{name}";
                pageDataList.Add(page);

                // Search GoodReads author page for Bio
                var docg = await web.LoadFromWebAsync(page.Url);
                page.Url = docg.DocumentNode.SelectSingleNode("//meta[@property='og:url']").Attributes["content"].Value;
                var authorBioId = "freeTextauthor" + findNumber(page.Url);
                var authorBioNode = docg.DocumentNode.SelectSingleNode($"//span[@id=\"{authorBioId}\"]");
                authorData.BioHtml = authorBioNode.InnerHtml;
                page.BioHtml = authorBioNode.InnerHtml;
                page.ImageUrl = docg.DocumentNode.SelectSingleNode("//meta[@property='og:image']").Attributes["content"].Value;
                page.Description = docg.DocumentNode.SelectSingleNode("//meta[@property='og:description']").Attributes["content"].Value;
            }

            catch
            {
                return authorData;
            }

            authorData.Images = imageDataList;
            authorData.Pages = pageDataList;

            return authorData;
        }

        public async Task<SeriesData> GetSeriesDataAsync(string name)
        {
            SeriesData seriesdata = new SeriesData();
            seriesdata.Name = name;

            var web = new HtmlWeb();
            web.UserAgent = desktopAgent;

            var doc = await web.LoadFromWebAsync($"{bookseriesinorderBaseUrl}{name.Replace(' ','-')}");
            var nodes = doc.DocumentNode.SelectNodes(@"//*[@id='site-content']/div/p");
            foreach (var node in nodes)
            {
                seriesdata.DescriptionHtml += node.OuterHtml;
            }

            seriesdata.BookCount = doc.DocumentNode.SelectSingleNode("//div[contains(@class,'list')]/table").ChildNodes.Count;

            return seriesdata;
        }

        public class WebScrapeException : Exception
        {
            public WebScrapeException(string message)
               : base(message)
            {
            }
        }

        private string findNumber(string input)
        {
            string num = string.Empty;
            for (int i = 0; i < input.Length; i++)
            {
                if (Char.IsDigit(input[i]))
                    num += input[i];
            }
            return num;
        }

        private (Bitmap, string) bitmapFromUrl(string url)
        {
            Bitmap bmp;
            string encoded;
            using (WebClient wc = new WebClient())
            {
                using (Stream s = wc.OpenRead(url))
                {
                    bmp = new Bitmap(s);
                    using (MemoryStream ms = new MemoryStream())
                    {
                        bmp.Save(ms, ImageFormat.Png);
                        encoded = "data:image/png;base64," + Convert.ToBase64String(ms.ToArray());
                    }
                }
            }
            return (bmp, encoded);
        }
    }
}
