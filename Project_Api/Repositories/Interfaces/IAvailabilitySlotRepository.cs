using ProjectApi.Models;

namespace Project_Api.Repositories.Interfaces
{
    public interface IAvailabilitySlotRepository : IRepository<AvailabilitySlot>
    {
        Task<IEnumerable<AvailabilitySlot>> GetByTherapistIdAsync(int therapistId);
        Task<AvailabilitySlot?> GetByIdWithBookingStatusAsync(int id);
    }
}
