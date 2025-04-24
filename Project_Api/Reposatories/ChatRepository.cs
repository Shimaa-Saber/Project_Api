using Project_Api.Interfaces;
using ProjectApi.Models;
using static Project_Api.Reposatories.ChatRepository;

namespace Project_Api.Reposatories
{
    public class ChatRepository:Chats
    {
      
            private readonly ApplicationDbContext _context;

            public ChatRepository(ApplicationDbContext context)
            {
                _context = context;
            }

            public List<Chat> GetAll() =>
                _context.Chats.ToList();

            public Chat GetById(int id) =>
                _context.Chats.Find(id);

            //public List<Chat> GetActiveChats(int userId) =>
            //    _context.Chats
            //        .Where(c => (c.ClientId == userId || c.TherapistId == userId) &&
            //                    c.IsActive)
            //        .ToList();

            //public Chat GetByParticipants(int clientId, int therapistId) =>
            //    _context.Chats
            //        .FirstOrDefault(c => c.ClientId == clientId &&
            //                           c.TherapistId == therapistId);

            public void insert(Chat obj) =>
                _context.Chats.Add(obj);

            public void Update(Chat obj) =>
                _context.Chats.Update(obj);

            public void Delete(Chat obj) =>
                _context.Chats.Remove(obj);

            public void Save() => _context.SaveChanges();
        }
    }

