using Microsoft.EntityFrameworkCore;
using Project_Api.Repositories.Interfaces;
using ProjectApi.Models;

namespace Project_Api.Repositories.Implementations
{
    public class VideoSessionDetailsRepository : IVideoSessionDetailsRepository
    {
        private readonly ApplicationDbContext _context;
        public VideoSessionDetailsRepository(ApplicationDbContext context) => _context = context;

        public async Task<VideoSessionDetail?> GetBySessionIdAsync(int sessionId)
            => await _context.VideoSessionDetails.FirstOrDefaultAsync(v => v.SessionId == sessionId);
        public async Task AddAsync(VideoSessionDetail details) => await _context.VideoSessionDetails.AddAsync(details);
        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
    }
}
