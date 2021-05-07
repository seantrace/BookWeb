using BookWeb.Client.Pages.Utilities;
using BookWeb.Client.Services;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookWeb.Client.Shared.Components
{
    public enum CalibreRecordType
    {
        Author,
        Category,
        Series,
        Publisher,
        Language,
        Format
    }

    public partial class CalibreRecordViewer
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public ICalibreService CalibreService { get; set; }

        [Parameter]
        public string Title { get; set; }

        [Parameter]
        public String Icon { get; set; }

        [Parameter]
        public string selectedRecordLetter { get; set; }

        [Parameter]
        public CalibreRecordType CalibreRecordType { get; set; }

        [Parameter]
        public string BaseUrl { get; set; }

        [Parameter]
        public string DetailsUrl { get; set; }

        char selectedRecordChar = ' ';
        private List<ICalibreRecord> records;
        private Dictionary<char, List<ICalibreRecord>> recordByChar;
        string RecordCount = "";
        bool _sortDirection = true;
        bool SortDirection
        {
            get
            {
                return _sortDirection;
            }
            set
            {
                _sortDirection = value;
                if (selectedRecordChar != '*')
                {
                    List<ICalibreRecord> collection = recordByChar[selectedRecordChar];
                    if (value)
                    {
                        collection.Sort((x, y) => x.Sort.CompareTo(y.Sort));
                    }
                    else
                    {
                        collection.Sort((x, y) => y.Sort.CompareTo(x.Sort));
                    }
                }
            }
        }
        

        protected override async Task OnInitializedAsync()
        {
            switch (CalibreRecordType) {
                case CalibreRecordType.Author:
                    List<Author> author = await CalibreService.GetAuthors();
                    records = author.ToList<ICalibreRecord>();
                    break;
                case CalibreRecordType.Category:
                    List<Tag> tags = await CalibreService.GetTags();
                    records = tags.ToList<ICalibreRecord>();
                    break;
                case CalibreRecordType.Series:
                    List<Series> series = await CalibreService.GetSeries();
                    records = series.ToList<ICalibreRecord>();
                    break;
                case CalibreRecordType.Publisher:
                    List<Publisher> publishers = await CalibreService.GetPublishers();
                    records = publishers.ToList<ICalibreRecord>();
                    break;
                case CalibreRecordType.Language:
                    List<Language> languages = await CalibreService.GetLanguages();
                    records = languages.ToList<ICalibreRecord>();
                    break;
                case CalibreRecordType.Format:
                    List<Format> formats = await CalibreService.GetFormats();
                    records = formats.ToList<ICalibreRecord>();
                    break;
            }
            records.Sort((x, y) => x.Sort.CompareTo(y.Sort));
            recordByChar = new Dictionary<char, List<ICalibreRecord>>();
            RecordCount = $" [{records.Count}]";
            if (records.Count < 10)
            {
                recordByChar.Add('*', new List<ICalibreRecord>());
                recordByChar['*'].AddRange(records);
                selectedRecordLetter = "*";
                selectedRecordChar = '*';
            }
            else
            {
                foreach (ICalibreRecord record in records)
                {
                    char f = record.Sort.ToUpper().First();
                    if (!recordByChar.ContainsKey(f))
                    {
                        recordByChar.Add(f, new List<ICalibreRecord>());
                    }
                    recordByChar[f].Add(record);
                }
            }
        }

        protected override async Task OnParametersSetAsync()
        {
            if (selectedRecordLetter != null)
            {
                selectedRecordChar = selectedRecordLetter.First();
                // Ensure initial sort in in required order
                SortDirection = _sortDirection;
                foreach (ICalibreRecord record in recordByChar[selectedRecordChar])
                {
                    if (record.Books == null)
                    {
                        switch (CalibreRecordType)
                        {
                            case CalibreRecordType.Author:
                                record.Books = await CalibreService.GetBooksByAuthor(record.Id);
                                break;
                            case CalibreRecordType.Category:
                                record.Books = await CalibreService.GetBooksByTag(record.Id);
                                break;
                            case CalibreRecordType.Series:
                                record.Books = await CalibreService.GetBooksBySeries(record.Id);
                                break;
                            case CalibreRecordType.Publisher:
                                record.Books = await CalibreService.GetBooksByPublisher(record.Id);
                                break;
                            case CalibreRecordType.Language:
                                record.Books = await CalibreService.GetBooksByLanguage(record.Id);
                                break;
                            case CalibreRecordType.Format:
                                record.Books = await CalibreService.GetBooksByFormat(record.Id);
                                break;
                        }
                        
                    }
                }
                StateHasChanged();
            }
        }
    }
}
