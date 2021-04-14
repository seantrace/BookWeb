using BookWeb.Application.Models.Chat;
using BookWeb.Application.Responses.Identity;
using BookWeb.Shared.Wrapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookWeb.Application.Interfaces.Services
{
    public interface IChatService
    {
        Task<Result<IEnumerable<ChatUserResponse>>> GetChatUsersAsync(string userId);

        Task<IResult> SaveMessageAsync(ChatHistory message);

        Task<Result<IEnumerable<ChatHistoryResponse>>> GetChatHistoryAsync(string userId, string contactId);
    }
}