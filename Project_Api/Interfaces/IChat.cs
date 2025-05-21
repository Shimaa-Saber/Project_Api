using ProjectApi.Models;
using ProjectApi.Repositories;

namespace Project_Api.Interfaces
{
    public interface IChats 
    {
        Task<IEnumerable<Chat>> GetUserChatsAsync(string userId);
        Task<IEnumerable<Message>> GetChatMessagesAsync(int chatId, string userId);
        Task<Message> SendMessageAsync(int chatId, string senderId, string content);
        Task<bool> MarkMessageAsReadAsync(int messageId, string userId);
        Task<Chat> GetOrCreateChatAsync(string user1Id, string user2Id);
    }
}
