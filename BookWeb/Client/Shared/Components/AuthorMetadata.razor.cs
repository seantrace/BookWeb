using BookWeb.Client.Services;
using BookWeb.Shared.BookWebModels;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookWeb.Client.Shared.Components
{
    public partial class AuthorMetadata
    {
        [Inject]
        public ICalibreService CalibreService { get; set; }

        [Parameter]
        public string AuthorName { get; set; }

        private List<ImageData> imageData = null;

        protected override async Task OnInitializedAsync()
        {
            imageData = await CalibreService.GetAuthorImageUrls(AuthorName);
        }
    }
}
