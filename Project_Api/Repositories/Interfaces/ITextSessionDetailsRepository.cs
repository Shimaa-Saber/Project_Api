using ProjectApi.Models;

namespace Project_Api.Repositories.Interfaces
{
    public interface ITextSessionDetailsRepository
    {
        Task<TextSessionDetail?> GetBySessionIdAsync(int sessionId);
        Task AddAsync(TextSessionDetail details);
        Task SaveChangesAsync();
    }
}
