using Microsoft.EntityFrameworkCore;
using Project_Api.DTO;
using Project_Api.Interfaces;
using ProjectApi.Models;
using static Project_Api.Reposatories.AvailabilitySlotRepository;

namespace Project_Api.Reposatories
{
    public class AvailabilitySlotRepository : Slots
    {


        private readonly ApplicationDbContext _context;
        private readonly ILogger<AvailabilitySlotRepository> _logger;

        public AvailabilitySlotRepository(ApplicationDbContext context, ILogger<AvailabilitySlotRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public List<AvailabilitySlot> GetAll() =>
            _context.AvailabilitySlots.ToList();

        public AvailabilitySlot GetById(int id) =>
            _context.AvailabilitySlots.Find(id);

        //public List<AvailabilitySlot> GetAvailableSlots(int therapistId, DateTime date) =>
        //    _context.AvailabilitySlots
        //        .Where(s => s.TherapistId == therapistId &&
        //                   !s.IsBooked &&
        //                   s.Date.Date == date.Date)
        //        .OrderBy(s => s.StartTime)
        //        .ToList();

        //public List<AvailabilitySlot> GetByTherapist(int therapistId) =>
        //    _context.AvailabilitySlots
        //        .Where(s => s.TherapistId == therapistId)
        //        .ToList();

        public void insert(AvailabilitySlot obj) =>
            _context.AvailabilitySlots.Add(obj);

        public void Update(AvailabilitySlot obj) =>
            _context.AvailabilitySlots.Update(obj);

        public void Delete(AvailabilitySlot obj) =>
            _context.AvailabilitySlots.Remove(obj);

        public void Save() => _context.SaveChanges();

        public async Task<IEnumerable<AvailabilitySlotDto>> GetAvailableSlotsAsync(
        string therapistId,
        DateTime? startDate,
        DateTime? endDate,
        string slotType)
        {
            try
            {
                var fromDate = startDate ?? DateTime.Today;
                var toDate = endDate ?? fromDate.AddDays(14);

                var query = _context.AvailabilitySlots
                    .Where(s => s.TherapistId == therapistId &&
                               s.Date >= fromDate.Date &&
                               s.Date <= toDate.Date &&
                               !s.IsBooked);

                if (!string.IsNullOrEmpty(slotType))
                    query = query.Where(s => s.SlotType == slotType);

                return await query
                    .OrderBy(s => s.Date)
                    .ThenBy(s => s.StartTime)
                    .Select(s => new AvailabilitySlotDto
                    {
                        Id = s.Id,
                        Date = s.Date,
                        DayOfWeek = s.Date.DayOfWeek,
                        StartTime = s.StartTime,
                        EndTime = s.EndTime,
                        SlotType = s.SlotType
                    })
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching available slots");
                throw; // Let controller handle the exception
            }









        }
    }
}
