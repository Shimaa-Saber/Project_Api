using Project_Api.Interfaces;
using ProjectApi.Models;

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

        public List<Session> GetByTherapistId(int therapistId) =>
            _context.Sessions.Where(s => s.TherapistId == therapistId).ToList();

        public void insert(Session obj) => _context.Sessions.Add(obj);

        public void Update(Session obj) => _context.Sessions.Update(obj);

        public void Delete(Session obj) => _context.Sessions.Remove(obj);

        public void Save() => _context.SaveChanges();
    }
}
