using BookWeb.Application.Features.Dashboard.GetData;
using BookWeb.Client.Infrastructure.Extensions;
using BookWeb.Shared.Wrapper;
using System.Net.Http;
using System.Threading.Tasks;

namespace BookWeb.Client.Infrastructure.Managers.Dashboard
{
    public class DashboardManager : IDashboardManager
    {
        private readonly HttpClient _httpClient;

        public DashboardManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IResult<DashboardDataResponse>> GetDataAsync()
        {
            var response = await _httpClient.GetAsync(Routes.DashboardEndpoint.GetData);
            var data = await response.ToResult<DashboardDataResponse>();
            return data;
        }
    }
}