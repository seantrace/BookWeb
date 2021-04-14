using BookWeb.Application.Features.Dashboard.GetData;
using BookWeb.Shared.Wrapper;
using System.Threading.Tasks;

namespace BookWeb.Client.Infrastructure.Managers.Dashboard
{
    public interface IDashboardManager : IManager
    {
        Task<IResult<DashboardDataResponse>> GetDataAsync();
    }
}