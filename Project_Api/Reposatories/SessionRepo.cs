using Microsoft.EntityFrameworkCore;
using Project_Api.DTO.BookingSession;
using Project_Api.Interfaces;
using ProjectApi.Models;
using static Project_Api.Interfaces.Sessions;

namespace Project_Api.Reposatories
{
    public class SessionRepository : Sessions
    {
        private readonly ApplicationDbContext _context;

        public SessionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Session> GetAll() => _context.Sessions.ToList();

        public Session GetById(int id) => _context.Sessions.Find(id);

        //public List<Session> GetByTherapistId(int therapistId) =>
        //    _context.Sessions.Where(s => s.TherapistId == therapistId).ToList();

        public void insert(Session obj) => _context.Sessions.Add(obj);

        public void Update(Session obj) => _context.Sessions.Update(obj);

        public void Delete(Session obj) => _context.Sessions.Remove(obj);

        public void Save() => _context.SaveChanges();


        public async Task<SessionResult> BookSessionAsync(BookSessionDto dto)
        {
            await using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                // Validate slot
                var slot = await _context.AvailabilitySlots
                    .FirstOrDefaultAsync(s => s.Id == dto.AvailabilitySlotId && s.IsAvailable);

                if (slot == null)
                    return new SessionResult(false, null, "Slot not available");

                // Create session
                var session = new Session
                {
                    ClientId = dto.ClientId,
                    TherapistId = dto.TherapistId,
                    AvailabilitySlotId = dto.AvailabilitySlotId,
                    Type = dto.SessionType,
                    Notes = dto.Notes,
                    Status = SessionStatus.Confirmed,
                    CreatedAt = DateTime.UtcNow
                };

                // Update slot
                slot.IsAvailable = false;

                _context.Sessions.Add(session);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                // Fire-and-forget notification
               // _ = _notificationService.SendBookingConfirmationAsync(session.Id);

                return new SessionResult(true, session, null);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return new SessionResult(false, null, ex.Message);
            }
        }


        public async Task<CancelResult> CancelSessionAsync(int sessionId, string userId)
        {
            var session = await _context.Sessions
                .Include(s => s.AvailabilitySlot)
                .FirstOrDefaultAsync(s => s.Id == sessionId &&
                                       (s.ClientId == userId || s.TherapistId == userId));

            if (session == null)
                return new CancelResult(false, "Session not found");

            if (session.Status != SessionStatus.Confirmed)
                return new CancelResult(false, "Only confirmed sessions can be cancelled");

            // Business rule: 24h cancellation policy
            DateTime slotDateTime = session.AvailabilitySlot.Date.Add(session.AvailabilitySlot.StartTime);

            if (slotDateTime < DateTime.UtcNow.AddHours(24))
                return new CancelResult(false, "Cancellation window expired");

            session.Status = SessionStatus.Cancelled;
            session.AvailabilitySlot.IsAvailable = true;
            await _context.SaveChangesAsync();

            return new CancelResult(true, null);
        }




        public async Task<IEnumerable<Session>> GetUpcomingSessionsAsync(string userId)
        {
            return await _context.Sessions
     .Include(s => s.AvailabilitySlot)
     .Include(s => s.Client)
     .Include(s => s.Therapist)
     .Where(s => (s.ClientId == userId || s.TherapistId == userId) &&
                s.Status == SessionStatus.Confirmed &&
                s.AvailabilitySlot.Date > DateTime.UtcNow)
     .OrderBy(s => s.AvailabilitySlot.Date)
     .ToListAsync();
        }




        public async Task<IEnumerable<Session>> GetSessionHistoryAsync(string userId)
        {
            return await _context.Sessions
                .Include(s => s.AvailabilitySlot)
                .Where(s => (s.ClientId == userId || s.TherapistId == userId) &&
                            (s.Status == SessionStatus.Completed ||
                             s.Status == SessionStatus.Cancelled))
                .OrderByDescending(s => s.AvailabilitySlot.StartTime)
                .ToListAsync();
        }

    }
}
