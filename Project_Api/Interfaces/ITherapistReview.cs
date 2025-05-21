using Project_Api.DTO.Review;
using ProjectApi.Models;
using ProjectApi.Repositories;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Project_Api.Interfaces
{
    public interface ITherapistReviews 
    {
     //   public IQueryable<TherapistReview> Get(Expression<Func<TherapistReview, bool>> predicate);

        Task<TherapistReview> CreateReviewAsync(string clientId, CreateReviewDto dto);
        Task<TherapistReview> AddTherapistResponseAsync(int reviewId, string therapistId, ReviewResponseDto dto);
        Task<IEnumerable<TherapistReview>> GetUserReviewsAsync(string userId);
        Task<bool> DeleteReviewAsync(int reviewId, bool isAdmin);

    }
}
