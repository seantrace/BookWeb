using BookWeb.Application.Interfaces.Common;

namespace BookWeb.Application.Interfaces.Services
{
    public interface ICurrentUserService : IService
    {
        string UserId { get; }
    }
}