using Project_Api.DTO;
using Project_Api.DTO.BookingSession;
using ProjectApi.Models;
using ProjectApi.Repositories;
using System.Linq.Expressions;

namespace Project_Api.Interfaces
{
    public interface ISessions: IGenericRepository<Session>
    {

        Task<SessionResult> BookSessionAsync(BookSessionDto dto);
        Task<CancelResult> CancelSessionAsync(int sessionId, string userId);
         Task<IEnumerable<Session>> GetUpcomingSessionsAsync(string userId);
         Task<IEnumerable<Session>> GetSessionHistoryAsync(string userId);

        public record SessionResult(bool IsSuccess, Session? Session, string? Error);
        public record CancelResult(bool IsSuccess, string? Error);

        public IQueryable<Session> Get(Expression<Func<Session, bool>> predicate);


    }
}
