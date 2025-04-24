using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectApi.Models
{
    public class TherapistReview
    {
        public int Id { get; set; }
        [ForeignKey("Therapist")]
        public int TherapistId { get; set; }
       
        [ForeignKey("Session")]
        public int SessionId { get; set; }
        public int Rating { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public bool Anonymous { get; set; }
        public bool IsVerified { get; set; }
        public DateTime CreatedAt { get; set; }

       
        public User Therapist { get; set; }
        public Session Session { get; set; }
    }
}
