using Project_Api.Interfaces;
using ProjectApi.Models;
using static Project_Api.Reposatories.AvailabilitySlotRepository;

namespace Project_Api.Reposatories
{
    public class AvailabilitySlotRepository:Slots
    {

      
            private readonly ApplicationDbContext _context;

            public AvailabilitySlotRepository(ApplicationDbContext context)
            {
                _context = context;
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
        }
    }

