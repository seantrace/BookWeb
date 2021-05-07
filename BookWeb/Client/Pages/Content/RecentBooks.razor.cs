using BookWeb.Client.Services;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookWeb.Client.Pages.Content
{
    public partial class RecentBooks
    {
        [Inject]
        public ICalibreService CalibreService { get; set; }
        private List<Book> books;

        protected override async Task OnInitializedAsync()
        {
            books = await CalibreService.GetRecentBooks(27);
        }
    }
}
