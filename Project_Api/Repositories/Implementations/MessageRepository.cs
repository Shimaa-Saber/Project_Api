using Microsoft.EntityFrameworkCore;
using ProjectApi.Models;

namespace Project_Api.Repositories.Implementations
{
    public class MessageRepository
    {   private readonly ApplicationDbContext _context;
        public MessageRepository(ApplicationDbContext context) => _context = context;

        public async Task AddAsync(Message entity) => await _context.Messages.AddAsync(entity);
        public void Delete(Message entity) => _context.Messages.Remove(entity);
        public async Task<IEnumerable<Message>> GetAllAsync() => await _context.Messages.ToListAsync();
        public async Task<Message?> GetByIdAsync(int id) => await _context.Messages.FindAsync(id);
        public void Update(Message entity) => _context.Messages.Update(entity);
        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
        public async Task<IEnumerable<Message>> GetMessagesByChatIdAsync(int chatId)
            => await _context.Messages.Where(m => m.ChatId == chatId).ToListAsync();
        public async Task<IEnumerable<Message>> GetUnreadMessagesByUserIdAsync(int userId)
            => await _context.Messages.Where(m => !m.IsRead && m.SenderId != userId).ToListAsync();
    }
}
