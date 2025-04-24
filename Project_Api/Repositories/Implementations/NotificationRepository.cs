using Microsoft.EntityFrameworkCore;
using Project_Api.Repositories.Interfaces;
using ProjectApi.Models;

namespace Project_Api.Repositories.Implementations
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly ApplicationDbContext _context;
        public NotificationRepository(ApplicationDbContext context) => _context = context;

        public async Task AddAsync(Notification entity) => await _context.Notifications.AddAsync(entity);
        public void Delete(Notification entity) => _context.Notifications.Remove(entity);
        public async Task<IEnumerable<Notification>> GetAllAsync() => await _context.Notifications.ToListAsync();
        public async Task<Notification?> GetByIdAsync(int id) => await _context.Notifications.FindAsync(id);
        public void Update(Notification entity) => _context.Notifications.Update(entity);
        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
        public async Task<IEnumerable<Notification>> GetUnreadNotifications(int userId)
            => await _context.Notifications.Where(n => n.UserId == userId && !n.IsRead).ToListAsync();

    }
}
