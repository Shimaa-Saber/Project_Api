using Project_Api.Interfaces;
using ProjectApi.Models;
using static Project_Api.Reposatories.MessageRepository;

namespace Project_Api.Reposatories
{
    public class MessageRepository:Imessages
    {
        
            private readonly ApplicationDbContext _context;

            public MessageRepository(ApplicationDbContext context)
            {
                _context = context;
            }

            public List<Message> GetAll() =>
                _context.Messages.ToList();

            public Message GetById(int id) =>
                _context.Messages.Find(id);

            //public List<Message> GetUnreadMessages(int chatId, int userId) =>
            //    _context.Messages
            //        .Where(m => m.ChatId == chatId &&
            //                   m.SenderId != userId &&
            //                   !m.IsRead)
            //        .ToList();

            //public List<Message> GetChatHistory(int chatId) =>
            //    _context.Messages
            //        .Where(m => m.ChatId == chatId)
            //        .OrderBy(m => m.CreatedAt)
            //        .ToList();

            public void insert(Message obj) =>
                _context.Messages.Add(obj);

            public void Update(Message obj) =>
                _context.Messages.Update(obj);

            public void Delete(Message obj) =>
                _context.Messages.Remove(obj);

            public void Save() => _context.SaveChanges();
        }
    }

