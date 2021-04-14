using BookWeb.Application.Responses.Audit;
using BookWeb.Shared.Wrapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookWeb.Client.Infrastructure.Managers.Audit
{
    public interface IAuditManager : IManager
    {
        Task<IResult<IEnumerable<AuditResponse>>> GetCurrentUserTrailsAsync();

        Task<string> DownloadFileAsync();
    }
}