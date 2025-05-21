using Microsoft.EntityFrameworkCore;
using Project_Api.DTO.Review;
using Project_Api.Interfaces;
using ProjectApi.Models;
using System.Linq.Expressions;
using static Project_Api.Reposatories.ReviewRepository;

namespace Project_Api.Reposatories
{
    public class ReviewRepository : ITherapistReviews
    {


        private readonly ApplicationDbContext _context;
        private readonly ILogger<ReviewRepository> _logger;




        public ReviewRepository(ApplicationDbContext context, ILogger<ReviewRepository> logger)
        {
            _context = context;
            _logger = logger;
        }


        public async Task<TherapistReview> CreateReviewAsync(string clientId, CreateReviewDto dto)
        {
            var review = new TherapistReview
            {
                ClientId = clientId,
                TherapistId = dto.TherapistId,
                SessionId = dto.SessionId,
                Rating = dto.Rating,
                Content = dto.Comment,
                CreatedAt = DateTime.UtcNow
            };

            await _context.TherapistReviews.AddAsync(review);
            await _context.SaveChangesAsync();

            // Update therapist average rating (fire-and-forget)
            _ = UpdateTherapistRatingAsync(dto.TherapistId);

            return review;
        }



        public async Task<TherapistReview> AddTherapistResponseAsync(int reviewId, string therapistId, ReviewResponseDto dto)
        {
            var review = await _context.TherapistReviews
                .FirstOrDefaultAsync(r => r.Id == reviewId && r.TherapistId == therapistId);

            if (review == null) return null;

            review.TherapistResponse = dto.Response;
            review.ResponseAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return review;
        }


        public async Task<IEnumerable<TherapistReview>> GetUserReviewsAsync(string userId)
        {
            return await _context.TherapistReviews
                .Where(r => r.ClientId == userId || r.TherapistId == userId)
                .Include(r => r.Client)
                .Include(r => r.Therapist)
                .OrderByDescending(r => r.CreatedAt)
                .ToListAsync();
        }



        public async Task<bool> DeleteReviewAsync(int reviewId, bool isAdmin)
        {
            var review = await _context.TherapistReviews.FindAsync(reviewId);
            if (review == null) return false;


            if (!isAdmin && !string.IsNullOrEmpty(review.TherapistResponse))
            {
                return false;
            }

            _context.TherapistReviews.Remove(review);
            await _context.SaveChangesAsync();

            // Recalculate therapist rating if deleted
            if (!string.IsNullOrEmpty(review.TherapistId))
            {
                _ = UpdateTherapistRatingAsync(review.TherapistId);
            }

            return true;
        }

        public IQueryable<TherapistReview> Get(Expression<Func<TherapistReview, bool>> predicate)
        {
            return _context.TherapistReviews.Where(predicate);
        }



        private async Task UpdateTherapistRatingAsync(string therapistId)
        {
            try
            {
                var averageRating = await _context.TherapistReviews
                    .Where(r => r.TherapistId == therapistId)
                    .AverageAsync(r => (double?)r.Rating) ?? 0;

                var profile = await _context.TherapistProfiles
                    .FirstOrDefaultAsync(p => p.UserId == therapistId);

                if (profile != null)
                {
                    profile.AverageRating = (decimal)averageRating;
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating rating for therapist {therapistId}");
            }























        }

    }
}

