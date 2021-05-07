using BookWeb.Client.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookWeb.Client.Pages.Content
{
    public partial class Library
    {
        [Inject]
        public ICalibreService CalibreService { get; set; }
 
        private GetBookListResponseDto response;
        private List<Book> books = new List<Book>();
        private int page_count=1;

        private long preFetchStart = 1;
        protected override async Task OnInitializedAsync()
        {
            await FetchMoreBooks();
        }

        private async Task FetchMoreBooks()
        {
            response = await CalibreService.GetBooks(36, page_count);
            preFetchStart = response.Items.Last<Book>().Id + 1;
            books.AddRange(response.Items);
            page_count++;
            //books = books.OrderBy(o => o.Title).ToList();
        }
    }
}
