using Microsoft.EntityFrameworkCore;
using Project_Api.Repositories.Interfaces;
using ProjectApi.Models;

namespace Project_Api.Repositories.Implementations
{
    public class AvailabilitySlotRepository : IAvailabilitySlotRepository
    {
        private readonly ApplicationDbContext _context;
        public AvailabilitySlotRepository(ApplicationDbContext context) => _context = context;

        public async Task AddAsync(AvailabilitySlot entity) => await _context.AvailabilitySlots.AddAsync(entity);
        public void Delete(AvailabilitySlot entity) => _context.AvailabilitySlots.Remove(entity);
        public async Task<IEnumerable<AvailabilitySlot>> GetAllAsync() => await _context.AvailabilitySlots.ToListAsync();
        public async Task<AvailabilitySlot?> GetByIdAsync(int id) => await _context.AvailabilitySlots.FindAsync(id);
        public void Update(AvailabilitySlot entity) => _context.AvailabilitySlots.Update(entity);
        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
        public async Task<IEnumerable<AvailabilitySlot>> GetByTherapistIdAsync(int therapistId)
            => await _context.AvailabilitySlots.Where(s => s.TherapistId == therapistId).ToListAsync();
        public async Task<AvailabilitySlot?> GetByIdWithBookingStatusAsync(int id)
            => await _context.AvailabilitySlots.FirstOrDefaultAsync(s => s.Id == id);
    }
}

