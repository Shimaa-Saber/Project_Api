using ProjectApi.Models;

namespace Project_Api.Repositories.Interfaces
{
    public interface IAudioSessionDetailsRepository
    {
        Task<AudioSessionDetail?> GetBySessionIdAsync(int sessionId);
        Task AddAsync(AudioSessionDetail details);
        Task SaveChangesAsync();
    }
}
