using Microsoft.EntityFrameworkCore;
using Project_Api.Repositories.Interfaces;
using ProjectApi.Models;

namespace Project_Api.Repositories.Implementations
{
    public class TherapistSpecializationRepository : ITherapistSpecializationRepository
    {
        private readonly ApplicationDbContext _context;
        public TherapistSpecializationRepository(ApplicationDbContext context) => _context = context;

        public async Task AddAsync(TherapistSpecialization entity) => await _context.TherapistSpecializations.AddAsync(entity);
        public void Delete(TherapistSpecialization entity) => _context.TherapistSpecializations.Remove(entity);
        public async Task<IEnumerable<TherapistSpecialization>> GetAllAsync() => await _context.TherapistSpecializations.ToListAsync();
        public async Task<TherapistSpecialization?> GetByIdAsync(int id) => await _context.TherapistSpecializations.FindAsync(id);
        public void Update(TherapistSpecialization entity) => _context.TherapistSpecializations.Update(entity);
        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
        public async Task<IEnumerable<TherapistSpecialization>> GetByTherapistIdAsync(int therapistId)
            => await _context.TherapistSpecializations.Where(ts => ts.TherapistId == therapistId).ToListAsync();
    }
}
