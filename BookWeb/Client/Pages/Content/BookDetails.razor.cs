using BookWeb.Client.Services;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookWeb.Client.Pages.Content
{
    public partial class BookDetails
    {
        [Inject]
        public ICalibreService CalibreService { get; set; }
        [Parameter]
        public string Id { get; set; }

        private Book book;

        protected override async Task OnInitializedAsync()
        {
            book = await CalibreService.GetBook(int.Parse(Id));
        }

        public string downloadUrl
        {
            get
            {
                return  $"https://localhost:5001/api/Books/{book.Id}/Download";
            }
        }

        private (string url, string name) GetIdentifierData(Identifier identifier)
        {
            string url = "";
            string name = "";
            switch (identifier.Type)
            {
                case "google":
                    url = $"https://books.google.com/books?id={identifier.Val}";
                    name = "Google Books";
                    break;
                case "isbn":
                    url = $"https://www.worldcat.org/isbn/{identifier.Val}";
                    name = "ISBN";
                    break;
                case "amazon":
                    url = $"https://amzn.com/{identifier.Val}";
                    name = "Amazon";
                    break;
                case "goodreads":
                    url = $"https://www.goodreads.com/book/show/{identifier.Val}";
                    name = "Goodreads";
                    break;
                default:
                    Console.WriteLine(identifier.Type);
                    break;
            }
            return (url,name);
        }

        public int Thickness
        {
            get
            {
                var rand = new Random();
                return 50;// return rand.Next(20, 100);
            }
        }
    }
}
