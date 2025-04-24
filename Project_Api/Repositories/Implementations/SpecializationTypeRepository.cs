using Microsoft.EntityFrameworkCore;
using Project_Api.Repositories.Interfaces;
using ProjectApi.Models;

namespace Project_Api.Repositories.Implementations
{
    public class SpecializationTypeRepository : ISpecializationTypeRepository
    {
        private readonly ApplicationDbContext _context;
        public SpecializationTypeRepository(ApplicationDbContext context) => _context = context;

        public async Task AddAsync(SpecializationType entity) => await _context.SpecializationTypes.AddAsync(entity);
        public void Delete(SpecializationType entity) => _context.SpecializationTypes.Remove(entity);
        public async Task<IEnumerable<SpecializationType>> GetAllAsync() => await _context.SpecializationTypes.ToListAsync();
        public async Task<SpecializationType?> GetByIdAsync(int id) => await _context.SpecializationTypes.FindAsync(id);
        public void Update(SpecializationType entity) => _context.SpecializationTypes.Update(entity);
        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
        public async Task<IEnumerable<SpecializationType>> GetByCategoryAsync(string category)
            => await _context.SpecializationTypes.Where(s => s.Category == category).ToListAsync();
    }
}
