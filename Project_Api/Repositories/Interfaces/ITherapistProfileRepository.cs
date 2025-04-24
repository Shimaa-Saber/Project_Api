using ProjectApi.Models;

namespace Project_Api.Repositories.Interfaces
{
    public interface ITherapistProfileRepository : IRepository<TherapistProfile>
    {
        Task<TherapistProfile?> GetByUserIdAsync(int userId);
    }
}
