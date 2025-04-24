using ProjectApi.Models;

namespace Project_Api.Repositories.Interfaces
{
    public interface IVideoSessionDetailsRepository
    {
        Task<VideoSessionDetail?> GetBySessionIdAsync(int sessionId);
        Task AddAsync(VideoSessionDetail details);
        Task SaveChangesAsync();
    }
}
