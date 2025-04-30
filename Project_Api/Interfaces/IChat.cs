using ProjectApi.Models;
using ProjectApi.Repositories;

namespace Project_Api.Interfaces
{
    public interface IChats : IGenericRepository<Chat>
    {
        Task<IEnumerable<Chat>> GetChatsByUserIdAsync(string userId);
    }
}
