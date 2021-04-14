using BookWeb.Application.Features.Products.Commands.AddEdit;
using BookWeb.Application.Features.Products.Queries.GetAllPaged;
using BookWeb.Application.Requests.Catalog;
using BookWeb.Shared.Wrapper;
using System.Threading.Tasks;

namespace BookWeb.Client.Infrastructure.Managers.Catalog.Product
{
    public interface IProductManager : IManager
    {
        Task<PaginatedResult<GetAllPagedProductsResponse>> GetProductsAsync(GetAllPagedProductsRequest request);

        Task<IResult<string>> GetProductImageAsync(int id);

        Task<IResult<int>> SaveAsync(AddEditProductCommand request);

        Task<IResult<int>> DeleteAsync(int id);

        Task<string> ExportToExcelAsync();
    }
}