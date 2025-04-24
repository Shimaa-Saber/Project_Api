using ProjectApi.Models;

namespace Project_Api.Repositories.Interfaces
{
    public interface ITherapistSpecializationRepository : IRepository<TherapistSpecialization>
    {
        Task<IEnumerable<TherapistSpecialization>> GetByTherapistIdAsync(int therapistId);
    }
}
