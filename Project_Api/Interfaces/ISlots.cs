using Project_Api.DTO;
using ProjectApi.Models;
using ProjectApi.Repositories;

namespace Project_Api.Interfaces
{
    public interface ISlots : IGenericRepository<AvailabilitySlot>
    {
        Task<IEnumerable<AvailabilitySlotDto>> GetAvailableSlotsAsync(
       string therapistId,
       DateTime? startDate,
       DateTime? endDate,
       string slotType);
    }
}
