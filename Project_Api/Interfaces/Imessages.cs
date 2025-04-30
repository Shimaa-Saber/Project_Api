using ProjectApi.Models;
using ProjectApi.Repositories;

namespace Project_Api.Interfaces
{
    public interface Imessages : IGenericRepository<Message>
    {
        Task<IEnumerable<Message>> GetMessagesByChatIdAsync(int chatId);
        Task<Message> SendMessageAsync(int chatId, string senderId, string content);
        Task MarkAsReadAsync(int messageId);
    }
}
