using Project_Api.Interfaces;
using ProjectApi.Models;
using static Project_Api.Reposatories.TextSessionRepository;

namespace Project_Api.Reposatories
{
    public class TextSessionRepository
    {
        public class TextSessionRepositoryS : TextSession
        {
            private readonly ApplicationDbContext _context;

            public TextSessionRepositoryS(ApplicationDbContext context)
            {
                _context = context;
            }

            public List<TextSessionDetail> GetAll() =>
                _context.TextSessionDetails.ToList();

            public TextSessionDetail GetById(int id) =>
                _context.TextSessionDetails.Find(id);

            //public List<TextSessionDetail> GetWithUnreadMessages() =>
            //    _context.TextSessionDetails
            //        .Where(t => t.Transcript.Any(m => !m.))
            //        .ToList();

            public void insert(TextSessionDetail obj) =>
                _context.TextSessionDetails.Add(obj);

            public void Update(TextSessionDetail obj) =>
                _context.TextSessionDetails.Update(obj);

            public void Delete(TextSessionDetail obj) =>
                _context.TextSessionDetails.Remove(obj);

            public void Save() => _context.SaveChanges();
        }
    }
}
