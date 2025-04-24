using Microsoft.EntityFrameworkCore;
using Project_Api.Repositories.Interfaces;
using ProjectApi.Models;

namespace Project_Api.Repositories.Implementations
{
    public class ChatRepository : IChatRepository
    {
        private readonly ApplicationDbContext _context;

        public ChatRepository(ApplicationDbContext context) => _context = context;

        public async Task AddAsync(Chat entity)
        {
            await _context.Chats.AddAsync(entity);
        }

        public void Delete(Chat entity)
        {
            _context.Chats.Remove(entity);
        }

        public async Task<IEnumerable<Chat>> GetAllAsync()
        {
            return await _context.Chats.ToListAsync();
        }

        public async Task<Chat?> GetByIdAsync(int id)
        {
            return await _context.Chats.FindAsync(id);
        }

        public void Update(Chat entity)
        {
            _context.Chats.Update(entity);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<Chat?> GetActiveChatAsync(int clientId, int therapistId)
        {
            return await _context.Chats
                .FirstOrDefaultAsync(c => c.ClientId == clientId && c.TherapistId == therapistId && c.IsActive);
        }
    }
}
