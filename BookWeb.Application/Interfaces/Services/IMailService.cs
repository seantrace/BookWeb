using BookWeb.Application.Requests.Mail;
using System.Threading.Tasks;

namespace BookWeb.Application.Interfaces.Services
{
    public interface IMailService
    {
        Task SendAsync(MailRequest request);
    }
}