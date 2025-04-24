using ProjectApi.Models;

namespace Project_Api.Repositories.Interfaces
{
    public interface ISessionRepository
    {
        Task<IEnumerable<Session>> GetSessionsByUserIdAsync(int userId);
    }
}
