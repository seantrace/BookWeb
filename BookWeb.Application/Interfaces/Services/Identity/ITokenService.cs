using BookWeb.Application.Interfaces.Common;
using BookWeb.Application.Requests.Identity;
using BookWeb.Application.Responses.Identity;
using BookWeb.Shared.Wrapper;
using System.Threading.Tasks;

namespace BookWeb.Application.Interfaces.Services.Identity
{
    public interface ITokenService : IService
    {
        Task<Result<TokenResponse>> LoginAsync(TokenRequest model);

        Task<Result<TokenResponse>> GetRefreshTokenAsync(RefreshTokenRequest model);
    }
}