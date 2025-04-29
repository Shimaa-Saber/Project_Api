using ProjectApi.Models;
using ProjectApi.Repositories;

namespace Project_Api.Interfaces
{
    public interface INotifications : IGenericRepository<Notification>
    {
        Task<List<Notification>> GetUnreadNotificationsAsync(string userId);
        Task<int> MarkNotificationsAsReadAsync(string userId, List<int> notificationIds);
        Task<int> MarkAllNotificationsAsReadAsync(string userId);
        Task SendNotificationAsync(string userId, string title, string message);
        Task SendBookingConfirmationAsync(int sessionId);
    }
}
