using Microsoft.EntityFrameworkCore;
using Project_Api.Repositories.Interfaces;
using ProjectApi.Models;

namespace Project_Api.Repositories.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context) => _context = context;

        public async Task AddAsync(User entity) => await _context.Users.AddAsync(entity);
        public void Delete(User entity) => _context.Users.Remove(entity);
        public async Task<IEnumerable<User>> GetAllAsync() => await _context.Users.ToListAsync();
        public async Task<User?> GetByIdAsync(int id) => await _context.Users.FindAsync(id);
        public void Update(User entity) => _context.Users.Update(entity);
        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
        public async Task<User?> GetByEmailAsync(string email) => await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
    }
}
