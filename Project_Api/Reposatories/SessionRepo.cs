using Microsoft.EntityFrameworkCore;
using Project_Api.DTO.BookingSession;
using Project_Api.Interfaces;
using ProjectApi.Models;
using System.Linq.Expressions;
using static Project_Api.Interfaces.ISessions;

namespace Project_Api.Reposatories
{
    public class SessionRepository : ISessions
    {
        private readonly ApplicationDbContext _context;
        private readonly INotifications _notificationService;
        private readonly ILogger<SessionRepository> _logger;

        public SessionRepository(ApplicationDbContext context, ILogger<SessionRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public List<Session> GetAll() => _context.Sessions.ToList();

        public Session GetById(int id) => _context.Sessions.Find(id);

        //public List<Session> GetByTherapistId(int therapistId) =>
        //    _context.ISessions.Where(s => s.TherapistId == therapistId).ToList();

        public void insert(Session obj) => _context.Sessions.Add(obj);

        public void Update(Session obj) => _context.Sessions.Update(obj);

        public void Delete(Session obj) => _context.Sessions.Remove(obj);

        public void Save() => _context.SaveChanges();


        public async Task<SessionResult> BookSessionAsync(BookSessionDto dto)
        {
            if (dto == null)
                return new SessionResult(false, null, "Invalid request data");

            try
            {
                var slot = await _context.AvailabilitySlots
                    .FirstOrDefaultAsync(s => s.Id == dto.AvailabilitySlotId && s.IsAvailable);

                if (slot == null)
                    return new SessionResult(false, null, "Slot not available");

                slot.IsAvailable = false;
                var session = new Session
                {
                    ClientId = dto.ClientId,
                    TherapistId = dto.TherapistId,
                    AvailabilitySlotId = dto.AvailabilitySlotId,
                    Type = dto.SessionType,
                    Status = SessionStatus.Confirmed,
                    CreatedAt = DateTime.UtcNow,
                    Notes = dto.Notes,

                };

                _context.Sessions.Add(session);
                await _context.SaveChangesAsync();

               
                //_ = _notificationService.SendBookingConfirmationAsync(session.Id)
                //    .ContinueWith(t =>
                //        _logger.LogError(t.Exception, "Notification failed"),
                //        TaskContinuationOptions.OnlyOnFaulted);

                return new SessionResult(true, session, null);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Booking failed");
                return new SessionResult(false, null, "Booking error occurred");
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

        public IQueryable<Session> Get(Expression<Func<Session, bool>> predicate)
        {
            return _context.Sessions.Where(predicate);
        }

    }
}
