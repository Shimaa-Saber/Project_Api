using Project_Api.Interfaces;
using ProjectApi.Models;
using static Project_Api.Reposatories.TherapistProfileRepository;

namespace Project_Api.Reposatories
{
    public class TherapistProfileRepository
    {
        public class TherapistProfileRepositorys : ITherabistProfile
        {
            private readonly ApplicationDbContext _context;

            public TherapistProfileRepositorys(ApplicationDbContext context)
            {
                _context = context;
            }

            public List<TherapistProfile> GetAll() =>
                _context.TherapistProfiles.ToList();

            public TherapistProfile GetById(int id) =>
                _context.TherapistProfiles.Find(id);

            //public TherapistProfile GetByUserId(int userId) =>
            //    _context.TherapistProfiles
            //        .FirstOrDefault(t => t.UserId == userId);

            public void insert(TherapistProfile obj) =>
                _context.TherapistProfiles.Add(obj);

            public void Update(TherapistProfile obj) =>
                _context.TherapistProfiles.Update(obj);

            public void Delete(TherapistProfile obj) =>
                _context.TherapistProfiles.Remove(obj);

            public void Save() => _context.SaveChanges();
        }
    }
}
