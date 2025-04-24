using ProjectApi.Models;

namespace Project_Api.Repositories.Interfaces
{
    public interface IMessageRepository : IRepository<Message>
    {
        Task<IEnumerable<Message>> GetMessagesByChatIdAsync(int chatId);
        Task<IEnumerable<Message>> GetUnreadMessagesByUserIdAsync(int userId);
    }
}
