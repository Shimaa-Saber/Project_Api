using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectApi.Models
{
    public class Payment
    {
        public int Id { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        [ForeignKey("Session")]
        public int SessionId { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string Status { get; set; }
        public string ReceiptUrl { get; set; }
        public DateTime CreatedAt { get; set; }

        public User User { get; set; }
        public Session Session { get; set; }
    }
}
