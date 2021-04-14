using BookWeb.Application.Requests;

namespace BookWeb.Application.Interfaces.Services
{
    public interface IUploadService
    {
        string UploadAsync(UploadRequest request);
    }
}