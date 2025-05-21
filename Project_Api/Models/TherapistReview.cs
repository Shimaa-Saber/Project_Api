using Project_Api.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectApi.Models
{
    public class TherapistReview
    {
        public int Id { get; set; }
        [ForeignKey("Therapist")]
        public string TherapistId { get; set; }
        [ForeignKey("Client")]
        public string ClientId { get; set; }

        [ForeignKey("Session")]
        public int SessionId { get; set; }
        public int Rating { get; set; }
        public string ?Title { get; set; }
        public string Content { get; set; }
        public bool ?Anonymous { get; set; }
        public bool IsVerified { get; set; }
        public DateTime? CreatedAt { get; set; }

        public ApplicationUser ?Therapist { get; set; }
        public ApplicationUser ?Client { get; set; }
        public Session? Session { get; set; }
        public string? TherapistResponse { get; internal set; }
        public DateTime ?ResponseAt { get; internal set; }
    }
}
