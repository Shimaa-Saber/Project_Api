using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project_Api.Interfaces;
using Project_Api.Reposatories;
using Project_Api.DTO;

namespace Project_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagingController : ControllerBase
    {
        private readonly IChats chatRepository;
        private readonly Imessages messageRepository;

        public MessagingController(IChats chatRepository, Imessages messageRepository)
        {
            this.chatRepository = chatRepository;
            this.messageRepository = messageRepository;
        }

        // 1. List my conversations
        [HttpGet("chats")]
        public async Task<IActionResult> GetMyChats()
        {
            var userId = GetCurrentUserId(); // now string
            var chats = await chatRepository.GetChatsByUserIdAsync(userId);
            return Ok(chats);
        }

        // 2. Get chat history
        [HttpGet("chats/{chatId}/messages")]
        public async Task<IActionResult> GetChatMessages(int chatId)
        {
            var messages = await messageRepository.GetMessagesByChatIdAsync(chatId);
            return Ok(messages);
        }

        // 3. Send message
        [HttpPost("chats/{chatId}/messages")]
        public async Task<IActionResult> SendMessage(int chatId, [FromBody] SendMessageDto messageDto)
        {
            var userId = GetCurrentUserId(); // now string
            var message = await messageRepository.SendMessageAsync(chatId, userId, messageDto.Content);
            return Ok(message);
        }

        // 4. Mark as read
        [HttpPut("messages/{messageId}/read")]
        public async Task<IActionResult> MarkMessageAsRead(int messageId)
        {
            await messageRepository.MarkAsReadAsync(messageId);
            return NoContent();
        }

        // ✅ تم التعديل هنا: ترجع string بدل int
        private string GetCurrentUserId()
        {
            return User.FindFirst("id")?.Value;
        }
    }
}
