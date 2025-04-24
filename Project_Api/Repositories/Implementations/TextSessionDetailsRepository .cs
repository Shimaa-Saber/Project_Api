using Microsoft.EntityFrameworkCore;
using Project_Api.Repositories.Interfaces;
using ProjectApi.Models;

namespace Project_Api.Repositories.Implementations
{
    public class TextSessionDetailsRepository : ITextSessionDetailsRepository
    {
        private readonly ApplicationDbContext _context;
        public TextSessionDetailsRepository(ApplicationDbContext context) => _context = context;

        public async Task<TextSessionDetail?> GetBySessionIdAsync(int sessionId)
            => await _context.TextSessionDetails.FirstOrDefaultAsync(t => t.SessionId == sessionId);
        public async Task AddAsync(TextSessionDetail details) => await _context.TextSessionDetails.AddAsync(details);
        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
    }
}
