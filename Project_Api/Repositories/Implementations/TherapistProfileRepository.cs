using Microsoft.EntityFrameworkCore;
using Project_Api.Repositories.Interfaces;
using ProjectApi.Models;

namespace Project_Api.Repositories.Implementations
{
    public class TherapistProfileRepository : ITherapistProfileRepository
    {
        private readonly ApplicationDbContext _context;
        public TherapistProfileRepository(ApplicationDbContext context) => _context = context;

        public async Task AddAsync(TherapistProfile entity) => await _context.TherapistProfiles.AddAsync(entity);
        public void Delete(TherapistProfile entity) => _context.TherapistProfiles.Remove(entity);
        public async Task<IEnumerable<TherapistProfile>> GetAllAsync() => await _context.TherapistProfiles.ToListAsync();
        public async Task<TherapistProfile?> GetByIdAsync(int id) => await _context.TherapistProfiles.FindAsync(id);
        public void Update(TherapistProfile entity) => _context.TherapistProfiles.Update(entity);
        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
        public async Task<TherapistProfile?> GetByUserIdAsync(int userId) => await _context.TherapistProfiles.FirstOrDefaultAsync(tp => tp.UserId == userId);
    }
}
