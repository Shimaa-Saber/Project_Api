using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Project_Api.DTO;
using Project_Api.Hubs;
using Project_Api.Interfaces;
using Project_Api.Reposatories;
using System.Security.Claims;

namespace Project_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagingController : ControllerBase
    {
        private readonly IChats _chatRepository;
        private readonly IHubContext<ChatHub> _hubContext;

        public MessagingController(IChats chatRepository, IHubContext<ChatHub> hubContext)
        {
            _chatRepository = chatRepository;
            _hubContext = hubContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetMyChats()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var chats = await _chatRepository.GetUserChatsAsync(userId);
            return Ok(chats);
        }

        [HttpGet("{id}/messages")]
        public async Task<IActionResult> GetChatMessages(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var messages = await _chatRepository.GetChatMessagesAsync(id, userId);
            return Ok(messages);
        }

        [HttpPost("{id}/messages")]
        public async Task<IActionResult> SendMessage(int id, [FromBody] SendMessageDto dto)
        {
            var senderId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var message = await _chatRepository.SendMessageAsync(id, senderId, dto.Content);

            if (message == null)
                return BadRequest("Invalid chat or sender");

           
            await _hubContext.Clients.Group($"user-{message.Chat.ClientId}")
                .SendAsync("ReceiveMessage", message);
            await _hubContext.Clients.Group($"user-{message.Chat.TherapistId}")
                .SendAsync("ReceiveMessage", message);

            return Ok(message);
        }

        [HttpPut("messages/{id}/read")]
        public async Task<IActionResult> MarkAsRead(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var success = await _chatRepository.MarkMessageAsReadAsync(id, userId);

            if (!success)
                return BadRequest("Message not found or already read");

          
            await _hubContext.Clients.User(userId)
                .SendAsync("MessageRead", id);

            return NoContent();
        }
    }

}
