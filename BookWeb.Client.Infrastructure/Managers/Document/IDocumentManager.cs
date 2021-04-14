using BookWeb.Application.Features.Documents.Commands.AddEdit;
using BookWeb.Application.Features.Documents.Queries.GetAll;
using BookWeb.Application.Requests.Documents;
using BookWeb.Shared.Wrapper;
using System.Threading.Tasks;

namespace BookWeb.Client.Infrastructure.Managers.Document
{
    public interface IDocumentManager : IManager
    {
        Task<PaginatedResult<GetAllDocumentsResponse>> GetAllAsync(GetAllPagedDocumentsRequest request);

        Task<IResult<int>> SaveAsync(AddEditDocumentCommand request);

        Task<IResult<int>> DeleteAsync(int id);
    }
}