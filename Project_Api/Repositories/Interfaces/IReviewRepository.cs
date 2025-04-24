using ProjectApi.Models;

namespace Project_Api.Repositories.Interfaces
{
    public interface IReviewRepository : IRepository<TherapistReview>
    {
        Task<IEnumerable<TherapistReview>> GetReviewsForTherapist(int therapistId);
    }
}
