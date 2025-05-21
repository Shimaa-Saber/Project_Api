using Microsoft.EntityFrameworkCore;
using Project_Api.Interfaces;
using ProjectApi.Models;
using System;
using static Project_Api.Reposatories.ChatRepository;

namespace Project_Api.Reposatories
{
    public class ChatRepository : IChats
    {

        private readonly ApplicationDbContext _context;
        private readonly ILogger<ChatRepository> _logger;

        public ChatRepository(ApplicationDbContext context, ILogger<ChatRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<Chat>> GetUserChatsAsync(string userId)
        {
            return await _context.Chats
                .Where(c => c.ClientId == userId || c.TherapistId == userId)
                .Include(c => c.Client)
                .Include(c => c.Therapist)
                .Select(c => new Chat
                {
                    
                    Id = c.Id,
                    ClientId = c.ClientId,
                    TherapistId = c.TherapistId,
                    CreatedAt = c.CreatedAt,
                    LastMessageAt = c.LastMessageAt,
                    Client = c.Client,
                    Therapist = c.Therapist,
                    Messages = c.Messages
                       
                        .Take(1)
                        .ToList()
                })
               
                .ToListAsync();
        }




        public async Task<IEnumerable<Message>> GetChatMessagesAsync(int chatId, string userId)
        {
            return await _context.Messages
                .Where(m => m.ChatId == chatId &&
                           (m.Chat.ClientId == userId || m.Chat.TherapistId == userId))
                .Include(m => m.Sender)
                
                .ToListAsync();
        }




        public async Task<Message> SendMessageAsync(int chatId, string senderId, string content)
        {
            var chat = await _context.Chats
                .FirstOrDefaultAsync(c => c.Id == chatId &&
                                        (c.ClientId == senderId || c.TherapistId == senderId));

            if (chat == null) return null;

            var message = new Message
            {
               // Id = Guid.NewGuid(),
                ChatId = chatId,
                SenderId = senderId,
                Content = content,
               
                IsRead = false
            };

            chat.LastMessageAt = DateTime.UtcNow;

            await _context.Messages.AddAsync(message);
            await _context.SaveChangesAsync();

            return message;
        }




        public async Task<bool> MarkMessageAsReadAsync(int messageId, string userId)
        {
            var message = await _context.Messages
                .Include(m => m.Chat)
                .FirstOrDefaultAsync(m => m.Id == messageId &&
                                         (m.Chat.ClientId == userId || m.Chat.TherapistId == userId));

            if (message == null || message.IsRead) return false;

            message.IsRead = true;
            await _context.SaveChangesAsync();
            return true;
        }



        public async Task<Chat> GetOrCreateChatAsync(string user1Id, string user2Id)
        {
            var chat = await _context.Chats
                .FirstOrDefaultAsync(c =>
                    (c.ClientId == user1Id && c.TherapistId == user2Id) ||
                    (c.ClientId == user2Id && c.TherapistId == user1Id));

            if (chat == null)
            {
                chat = new Chat
                {
                    //Id = Guid.NewGuid(),
                    ClientId = user1Id,
                    TherapistId = user2Id
                };
                await _context.Chats.AddAsync(chat);
                await _context.SaveChangesAsync();
            }

            return chat;
        }

       



    }
}

