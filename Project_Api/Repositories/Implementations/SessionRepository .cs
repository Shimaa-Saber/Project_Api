using Microsoft.EntityFrameworkCore;
using Project_Api.Repositories.Interfaces;
using ProjectApi.Models;

namespace Project_Api.Repositories.Implementations
{
    public class SessionRepository : ISessionRepository
    {
        private readonly ApplicationDbContext _context;
        public SessionRepository(ApplicationDbContext context) => _context = context;

        public async Task AddAsync(Session entity) => await _context.Sessions.AddAsync(entity);
        public void Delete(Session entity) => _context.Sessions.Remove(entity);
        public async Task<IEnumerable<Session>> GetAllAsync() => await _context.Sessions.ToListAsync();
        public async Task<Session?> GetByIdAsync(int id) => await _context.Sessions.FindAsync(id);
        public void Update(Session entity) => _context.Sessions.Update(entity);
        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
        public async Task<IEnumerable<Session>> GetSessionsByUserIdAsync(int userId)
            => await _context.Sessions.Where(s => s.ClientId == userId || s.TherapistId == userId).ToListAsync();
    }
}
