using BookWeb.Client.Pages.Utilities;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookWeb.Client.Shared.Components
{
    public partial class LetterSelector
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Parameter]
        public List<char> CharacterList { get; set; }
        [Parameter]
        public string UrlBase { get; set; }
        [Parameter]
        public char SelectedChipCharacter { get; set; }

        MudChipSet chipSet;
        Dictionary<char,MudChip> chips = new Dictionary<char, MudChip>();
        IAlphabetProvider provider = new EnglishAlphabetProvider();


        protected override void OnAfterRender(bool firstRender)
        {
            base.OnAfterRender(firstRender);
        }

        private void Goto(char letter)
        {
            NavigationManager.NavigateTo($"/{UrlBase }/{letter}");
        }
    }
}
