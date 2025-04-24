using ProjectApi.Models;

namespace Project_Api.Repositories.Interfaces
{
    public interface IChatRepository : IRepository<Chat>
    {
        Task<Chat?> GetActiveChatAsync(int clientId, int therapistId);
    }
}
