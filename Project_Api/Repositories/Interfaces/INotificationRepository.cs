using ProjectApi.Models;

namespace Project_Api.Repositories.Interfaces
{
    public interface INotificationRepository : IRepository<Notification>
    {
        Task<IEnumerable<Notification>> GetUnreadNotifications(int userId);
    }
}

