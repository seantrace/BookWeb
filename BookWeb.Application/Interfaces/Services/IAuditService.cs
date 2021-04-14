using BookWeb.Application.Responses.Audit;
using BookWeb.Shared.Wrapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookWeb.Application.Interfaces.Services
{
    public interface IAuditService
    {
        Task<IResult<IEnumerable<AuditResponse>>> GetCurrentUserTrailsAsync(string userId);

        Task<string> ExportToExcelAsync(string userId);
    }
}