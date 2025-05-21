using Project_Api.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectApi.Models
{
    public class Message
    {
        public int Id { get; set; }
        [ForeignKey("Chat")]
        public int ChatId { get; set; }
        [ForeignKey("Sender")]
        public string SenderId { get; set; }
        public string Content { get; set; }
        public bool IsRead { get; set; }
        public DateTime CreatedAt { get; set; }

        public Chat Chat { get; set; }
        public ApplicationUser Sender { get; set; }
      //  public object SentAt { get; internal set; }
    }
}
