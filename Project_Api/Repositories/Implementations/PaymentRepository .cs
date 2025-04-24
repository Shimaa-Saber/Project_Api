using Microsoft.EntityFrameworkCore;
using Project_Api.Repositories.Interfaces;
using ProjectApi.Models;

namespace Project_Api.Repositories.Implementations
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly ApplicationDbContext _context;
        public PaymentRepository(ApplicationDbContext context) => _context = context;

        public async Task AddAsync(Payment entity) => await _context.Payments.AddAsync(entity);
        public void Delete(Payment entity) => _context.Payments.Remove(entity);
        public async Task<IEnumerable<Payment>> GetAllAsync() => await _context.Payments.ToListAsync();
        public async Task<Payment?> GetByIdAsync(int id) => await _context.Payments.FindAsync(id);
        public void Update(Payment entity) => _context.Payments.Update(entity);
        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
        public async Task<IEnumerable<Payment>> GetPaymentsByUserId(int userId)
            => await _context.Payments.Where(p => p.UserId == userId).ToListAsync();
    }
}
