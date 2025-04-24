using Microsoft.EntityFrameworkCore;
using Project_Api.Repositories.Interfaces;
using ProjectApi.Models;

namespace Project_Api.Repositories.Implementations
{
    public class AudioSessionDetailsRepository : IAudioSessionDetailsRepository
    {
        private readonly ApplicationDbContext _context;
        public AudioSessionDetailsRepository(ApplicationDbContext context) => _context = context;

        public async Task<AudioSessionDetail?> GetBySessionIdAsync(int sessionId)
            => await _context.AudioSessionDetails.FirstOrDefaultAsync(a => a.SessionId == sessionId);
        public async Task AddAsync(AudioSessionDetail details) => await _context.AudioSessionDetails.AddAsync(details);
        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
    }
}
