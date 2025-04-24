using Project_Api.Interfaces;
using ProjectApi.Models;
using static Project_Api.Reposatories.NotificationRepository;

namespace Project_Api.Reposatories
{
    public class NotificationRepository:Notifications
    {
       
            private readonly ApplicationDbContext _context;

            public NotificationRepository(ApplicationDbContext context)
            {
                _context = context;
            }

            public List<Notification> GetAll() =>
                _context.Notifications.ToList();

            public Notification GetById(int id) =>
                _context.Notifications.Find(id);

            //public List<Notification> GetUnreadNotifications(int userId) =>
            //    _context.Notifications
            //        .Where(n => n.UserId == userId && !n.IsRead)
            //        .ToList();

            public void insert(Notification obj) =>
                _context.Notifications.Add(obj);

            public void Update(Notification obj) =>
                _context.Notifications.Update(obj);

            public void Delete(Notification obj) =>
                _context.Notifications.Remove(obj);

            public void Save() => _context.SaveChanges();
        }
    }

