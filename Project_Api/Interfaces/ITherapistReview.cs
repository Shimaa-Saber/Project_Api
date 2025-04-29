using ProjectApi.Models;
using ProjectApi.Repositories;
using System.Linq.Expressions;

namespace Project_Api.Interfaces
{
    public interface ITherapistReviews : IGenericRepository<TherapistReview>
    {
        public IQueryable<TherapistReview> Get(Expression<Func<TherapistReview, bool>> predicate);

    }
}
