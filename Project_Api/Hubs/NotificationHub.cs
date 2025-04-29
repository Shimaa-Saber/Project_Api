using Microsoft.AspNetCore.SignalR;
using Project_Api.Interfaces;
using System.Security.Claims;

namespace Project_Api.Hubs
{
    public class NotificationHub :Hub
    {
        private readonly IUserConnectionTracker _connectionTracker;

        public NotificationHub(IUserConnectionTracker connectionTracker)
        {
            _connectionTracker = connectionTracker;
        }

        public override async Task OnConnectedAsync()
        {
            var userId = Context.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId != null)
            {
                _connectionTracker.AddConnection(userId, Context.ConnectionId);
            }
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var userId = Context.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId != null)
            {
                _connectionTracker.RemoveConnection(userId, Context.ConnectionId);
            }
            await base.OnDisconnectedAsync(exception);
        }
    }
}
