using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Project_Api.Hubs;
using Project_Api.Interfaces;
using ProjectApi.Models;
using System;
using static Project_Api.Reposatories.NotificationRepository;

namespace Project_Api.Reposatories
{
    public class NotificationRepository:INotifications
    {
       
            private readonly ApplicationDbContext _context;
        private readonly IHubContext<NotificationHub> _hubContext;
        private readonly IUserConnectionTracker _connectionTracker;
        private readonly ILogger<NotificationRepository> _logger;
    

        public NotificationRepository(ApplicationDbContext context,IHubContext<NotificationHub> hubContext,
            IUserConnectionTracker userConnectionTracker, ILogger<NotificationRepository> logger)
            {
                _context = context;
            _hubContext = hubContext;
            _connectionTracker = userConnectionTracker;
            _logger = logger;
        }

            public List<Notification> GetAll() =>
                _context.Notifications.ToList();

            public Notification GetById(int id) =>
                _context.Notifications.Find(id);


       

       

        public async Task<List<Notification>> GetUnreadNotificationsAsync(string userId)
        {
            return await _context.Notifications
                .Where(n => n.UserId == userId && !n.IsRead)
                .OrderByDescending(n => n.CreatedAt)
                .ToListAsync();
        }


        public async Task<int> MarkNotificationsAsReadAsync(string userId, List<int> notificationIds)
        {
           
            var notifications = await _context.Notifications
                .Where(n => n.UserId == userId &&
                           !n.IsRead &&
                           notificationIds.Contains(n.Id))
                .ToListAsync();

            foreach (var notification in notifications)
            {
                notification.IsRead = true;
            }

            return await _context.SaveChangesAsync();  
        }


        public async Task<int> MarkAllNotificationsAsReadAsync(string userId)
        {
            
            var result = await _context.Notifications
                .Where(n => n.UserId == userId && !n.IsRead)
                .ExecuteUpdateAsync(setters =>
                    setters.SetProperty(n => n.IsRead, true));

            return result;  
        }


        public async Task SendNotificationAsync(string userId, string title, string message,)
        {
            var connections = _connectionTracker.GetConnections(userId);
            foreach (var connectionId in connections)
            {
                await _hubContext.Clients.Client(connectionId).SendAsync("ReceiveNotification",
                    new { Title = title, Message = message });
            }
        }



        public void insert(Notification obj) =>
                _context.Notifications.Add(obj);

            public void Update(Notification obj) =>
                _context.Notifications.Update(obj);

            public void Delete(Notification obj) =>
                _context.Notifications.Remove(obj);

            public void Save() => _context.SaveChanges();








        public async Task SendBookingConfirmationAsync(int sessionId)
        {
            try
            {
                var session = await _context.Sessions
                    .Include(s => s.AvailabilitySlot)
                    .Include(s => s.Client)
                    .Include(s => s.Therapist)
                    .FirstOrDefaultAsync(s => s.Id == sessionId);

                if (session == null)
                {
                    _logger.LogWarning($"Session {sessionId} not found for confirmation");
                    return;
                }

                // Prepare SignalR notification data
                var notificationData = new
                {
                    notificationType = "appointment",
                    data = new
                    {
                        sessionId = session.Id,
                        date = session.AvailabilitySlot.StartTime.ToString("yyyy-MM-dd"),
                        time = session.AvailabilitySlot.StartTime.ToString("HH:mm"),
                        therapistName = $"{session.Therapist.FullName} ",
                        sessionType = session.Type.ToString(),
                        duration = (session.AvailabilitySlot.EndTime - session.AvailabilitySlot.StartTime).TotalMinutes
                    },
                    isUrgent = false
                };

                // Notify the client via SignalR
                await NotifyUserViaSignalR(
                    session.ClientId,
                    "Session Confirmed",
                    notificationData);

                // Notify the therapist via SignalR (if needed)
                await NotifyUserViaSignalR(
                    session.TherapistId,
                    "New Session Booking",
                    notificationData);

                _logger.LogInformation($"Sent booking confirmation for session {sessionId}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to send booking confirmation for session {sessionId}");
            }
        }

        public async Task NotifyUserViaSignalR(string userId, string message, object data)
        {
            await _hubContext.Clients.User(userId).SendAsync("ReceiveNotification", new
            {
                Message = message,
                Data = data
            });
        }









    }
    }

