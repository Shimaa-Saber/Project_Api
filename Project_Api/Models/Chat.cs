using Project_Api.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectApi.Models
{
    public class Chat
    {
        public int Id { get; set; }
        [ForeignKey("Client")]
        public string ClientId { get; set; }
        [ForeignKey("Therapist")]
        public string TherapistId { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastMessageAt { get; set; }

        public ApplicationUser Client { get; set; }
        public ApplicationUser Therapist { get; set; }
        public ICollection<Message> Messages { get; set; }
    }
}
