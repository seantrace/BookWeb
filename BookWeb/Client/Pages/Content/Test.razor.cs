using Blazored.TextEditor;
using BookWeb.Client.Services;
using BookWeb.Shared.BookWebModels;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookWeb.Client.Pages.Content
{
    public partial class Test
    {
        public string SelectedOption { get; set; }
        [Inject]
        public ICalibreService CalibreService { get; set; }
        private AuthorData authorData = null;
        BlazoredTextEditor QuillHtml;
        string QuillHTMLContent;

        public async void GetHTML()
        {
            QuillHTMLContent = await this.QuillHtml.GetHTML();
            StateHasChanged();
        }

        public async void SetHTML()
        {
            string QuillContent = authorData.BioHtml;
            await this.QuillHtml.LoadHTMLContent(QuillContent);
            StateHasChanged();
        }
        private void Reset()
        {
            SelectedOption = null;
        }

        protected override async Task OnInitializedAsync()
        {
            authorData = await CalibreService.GetAuthorData("Lee Child");
        }
    }
}
