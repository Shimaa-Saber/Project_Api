using Project_Api.Interfaces;
using ProjectApi.Models;
using static Project_Api.Reposatories.ReviewRepository;

namespace Project_Api.Reposatories
{
    public class ReviewRepository:TherapistReviews
    {
        
        
            private readonly ApplicationDbContext _context;

            public ReviewRepository(ApplicationDbContext context)
            {
                _context = context;
            }

            public List<TherapistReview> GetAll() =>
                _context.TherapistReviews.ToList();

            public TherapistReview GetById(int id) =>
                _context.TherapistReviews.Find(id);

            //public List<TherapistReview> GetVerifiedReviews(int therapistId) =>
            //    _context.TherapistReviews
            //        .Where(r => r.TherapistId == therapistId && r.IsVerified)
            //        .ToList();

            public void insert(TherapistReview obj) =>
                _context.TherapistReviews.Add(obj);

            public void Update(TherapistReview obj) =>
                _context.TherapistReviews.Update(obj);

            public void Delete(TherapistReview obj) =>
                _context.TherapistReviews.Remove(obj);

            public void Save() => _context.SaveChanges();
        }
    }

