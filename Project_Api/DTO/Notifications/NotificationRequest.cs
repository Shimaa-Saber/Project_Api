using System.ComponentModel.DataAnnotations;

namespace Project_Api.DTO.Notifications
{
    public class NotificationRequest
    {
        [Required]
        public string UserId { get; set; } 

        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Title { get; set; }   

        [Required]
        [StringLength(500)]
        public string Message { get; set; } 

     
        public string NotificationType { get; set; } = "Info"; 
        public Dictionary<string, string> Data { get; set; }   
        public bool IsUrgent { get; set; } = false;
    }
}
