using ProjectApi.Models;

namespace Project_Api.DTO.Notifications
{
    public class NotificationDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public string Type { get; set; }
        public DateTime CreatedAt { get; set; }
      //  public Dictionary<string, string> Metadata { get; set; }
    }
}
