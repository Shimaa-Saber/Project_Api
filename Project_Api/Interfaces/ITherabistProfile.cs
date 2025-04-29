using ProjectApi.Models;
using ProjectApi.Repositories;
using System.Linq.Expressions;

namespace Project_Api.Interfaces
{
    public interface ITherabistProfile : IGenericRepository<TherapistProfile>
    {
        public IQueryable<TherapistProfile> Get(Expression<Func<TherapistProfile, bool>> predicate);
        public TherapistProfile GetById(string id);

    }
}
