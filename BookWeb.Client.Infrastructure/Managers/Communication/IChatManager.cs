using BookWeb.Application.Models.Chat;
using BookWeb.Application.Responses.Identity;
using BookWeb.Shared.Wrapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookWeb.Client.Infrastructure.Managers.Communication
{
    public interface IChatManager : IManager
    {
        Task<IResult<IEnumerable<ChatUserResponse>>> GetChatUsersAsync();

        Task<IResult> SaveMessageAsync(ChatHistory chatHistory);

        Task<IResult<IEnumerable<ChatHistoryResponse>>> GetChatHistoryAsync(string cId);
    }
}