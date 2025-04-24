using Microsoft.EntityFrameworkCore;
using Project_Api.Repositories.Interfaces;
using ProjectApi.Models;

namespace Project_Api.Repositories.Implementations
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly ApplicationDbContext _context;
        public ReviewRepository(ApplicationDbContext context) => _context = context;

        public async Task AddAsync(TherapistReview entity) => await _context.TherapistReviews.AddAsync(entity);
        public void Delete(TherapistReview entity) => _context.TherapistReviews.Remove(entity);
        public async Task<IEnumerable<TherapistReview>> GetAllAsync() => await _context.TherapistReviews.ToListAsync();
        public async Task<TherapistReview?> GetByIdAsync(int id) => await _context.TherapistReviews.FindAsync(id);
        public void Update(TherapistReview entity) => _context.TherapistReviews.Update(entity);
        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
        public async Task<IEnumerable<TherapistReview>> GetReviewsForTherapist(int therapistId)
            => await _context.TherapistReviews.Where(r => r.TherapistId == therapistId).ToListAsync();
    }
}
