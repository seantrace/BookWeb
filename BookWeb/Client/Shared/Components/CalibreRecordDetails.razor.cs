using BookWeb.Client.Services;
using BookWeb.Shared.BookWebModels;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookWeb.Client.Shared.Components
{
    public partial class CalibreRecordDetails
    {
        [Inject]
        public ICalibreService CalibreService { get; set; }
 
        [Parameter]
        public string Id { get; set; }
        [Parameter]
        public CalibreRecordType CalibreRecordType { get; set; }
        private ICalibreRecord record;
        private SeriesData seriesdata;

        protected override async Task OnInitializedAsync()
        {
            if (!String.IsNullOrEmpty(Id))
            {
                switch (CalibreRecordType)
                {
                    case CalibreRecordType.Author:
                        record = await CalibreService.GetAuthor(int.Parse(Id));
                        record.Books = await CalibreService.GetBooksByAuthor(record.Id);
                        break;
                    case CalibreRecordType.Category:
                        record = await CalibreService.GetTag(int.Parse(Id));
                        record.Books = await CalibreService.GetBooksByTag(record.Id);
                        break;
                    case CalibreRecordType.Series:
                        record = await CalibreService.GetSeries(int.Parse(Id));
                        record.Books = await CalibreService.GetBooksBySeries(record.Id);
                        seriesdata = await GetSeriesData(record.Name);
                        record.Books.Sort((x, y) => x.SeriesIndex.CompareTo(y.SeriesIndex));
                        break;
                    case CalibreRecordType.Publisher:
                        record = await CalibreService.GetPublisher(int.Parse(Id));
                        record.Books = await CalibreService.GetBooksByPublisher(record.Id);
                        break;
                    case CalibreRecordType.Language:
                        record = await CalibreService.GetLanguage(int.Parse(Id));
                        record.Books = await CalibreService.GetBooksByLanguage(record.Id);
                        break;
                    case CalibreRecordType.Format:
                        record = await CalibreService.GetFormat(int.Parse(Id));
                        record.Books = await CalibreService.GetBooksByFormat(record.Id);
                        break;
                }
            }
        }

        private async Task<SeriesData> GetSeriesData(string name)
        {
            SeriesData seriesdata = await CalibreService.GetSeriesData(name);
            return seriesdata;
        }
    }
}
