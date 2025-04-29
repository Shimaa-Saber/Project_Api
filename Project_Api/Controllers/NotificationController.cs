using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project_Api.DTO.Notifications;
using Project_Api.Interfaces;
using Project_Api.Reposatories;
using ProjectApi.Models;
using System.Security.Claims;
using System.Text.Json;

namespace Project_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    

    public class NotificationController : ControllerBase
    {
        private readonly INotifications _notificationRepository;
        private readonly ILogger<NotificationController> _logger;

        public NotificationController(INotifications notificationRepository, ILogger<NotificationController> logger)
        {
            _notificationRepository = notificationRepository;
            _logger = logger;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUnreadNotifications()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

           
            var notifications = await _notificationRepository.GetUnreadNotificationsAsync(userId);

         
            var response = notifications.Select(n => new NotificationDto
            {
                Id = n.Id,
                Title = n.Title,
                Message = n.Message,
                Type = n.Type,
                CreatedAt = n.CreatedAt,
               
            });

            return Ok(response);
        }



        [HttpPut("mark-read")]
        [Authorize]
        public async Task<IActionResult> MarkNotificationsAsRead([FromBody] MarkReadRequest request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            try
            {
                int affectedRecords;

                if (request.MarkAll)
                {
                    affectedRecords = await _notificationRepository.MarkAllNotificationsAsReadAsync(userId);
                }
                else
                {
                    if (request.NotificationIds == null || !request.NotificationIds.Any())
                    {
                        return BadRequest("Notification IDs are required when MarkAll is false");
                    }

                    affectedRecords = await _notificationRepository.MarkNotificationsAsReadAsync(
                        userId,
                        request.NotificationIds);
                }

                return Ok(new
                {
                    Message = "Notifications marked as read",
                    AffectedCount = affectedRecords
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error marking notifications as read");
                return StatusCode(500, "An error occurred");
            }
        }


        [HttpPost("notify")]
        public async Task<IActionResult> SendNotification(
    [FromBody] NotificationRequest request,
    [FromServices] INotifications notificationService)
        {
            await notificationService.SendNotificationAsync(
                request.UserId,
                request.Title,
                request.Message);

            return Ok();
        }
    }
}
