using Project_Api.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectApi.Models
{
    public enum NotificationType { Appointment, Message, System, Review }

    public class Notification
    {
        public int Id { get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; }
        public string Message { get; set; }
        public NotificationType Type { get; set; }
        public int RelatedId { get; set; }
        public bool IsRead { get; set; }
        public DateTime CreatedAt { get; set; }

        public ApplicationUser User { get; set; }
    }
}
