using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Project_Api.Interfaces;
using Project_Api.Reposatories;
using System.Text.RegularExpressions;

namespace Project_Api.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        private readonly IChats _chatRepository;
        private readonly ILogger<ChatHub> _logger;

        public ChatHub(IChats chatRepository, ILogger<ChatHub> logger)
        {
            _chatRepository = chatRepository;
            _logger = logger;
        }

        public override async Task OnConnectedAsync()
        {
            var userId = Context.UserIdentifier;
            await Groups.AddToGroupAsync(Context.ConnectionId, $"user-{userId}");
            await base.OnConnectedAsync();
        }

        public async Task SendMessage(int chatId, string content)
        {
            var senderId = Context.UserIdentifier;
            var message = await _chatRepository.SendMessageAsync(chatId, senderId, content);

            if (message != null)
            {
              
                await Clients.Group($"user-{message.Chat.ClientId}")
                    .SendAsync("ReceiveMessage", message);
                await Clients.Group($"user-{message.Chat.TherapistId}")
                    .SendAsync("ReceiveMessage", message);
            }
        }

        public async Task MarkAsRead(int messageId)
        {
            var userId = Context.UserIdentifier;
            var success = await _chatRepository.MarkMessageAsReadAsync(messageId, userId);

            if (success)
            {
                await Clients.User(userId)
                    .SendAsync("MessageRead", messageId);
            }
        }

    }
}

