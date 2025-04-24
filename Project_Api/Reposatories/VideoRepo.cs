using Project_Api.Interfaces;
using ProjectApi.Models;

namespace Project_Api.Reposatories
{
    public class VideoSessionRepository : VideoSession
    {
        private readonly ApplicationDbContext _context;

        public VideoSessionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<VideoSessionDetail> GetAll() =>
            _context.VideoSessionDetails.ToList();

        public VideoSessionDetail GetById(int id) =>
            _context.VideoSessionDetails.Find(id);

        public List<VideoSessionDetail> GetByPlatform(string platform) =>
            _context.VideoSessionDetails
                .Where(v => v.Platform == platform)
                .ToList();

        public void insert(VideoSessionDetail obj) =>
            _context.VideoSessionDetails.Add(obj);

        public void Update(VideoSessionDetail obj) =>
            _context.VideoSessionDetails.Update(obj);

        public void Delete(VideoSessionDetail obj) =>
            _context.VideoSessionDetails.Remove(obj);

        public void Save() => _context.SaveChanges();
    }
}
