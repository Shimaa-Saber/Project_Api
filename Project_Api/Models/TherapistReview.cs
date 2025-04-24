namespace ProjectApi.Models
{
    public class TherapistReview
    {
        public int Id { get; set; }
        public int TherapistId { get; set; }
        public int ClientId { get; set; }
        public int SessionId { get; set; }
        public int Rating { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public bool Anonymous { get; set; }
        public bool IsVerified { get; set; }
        public DateTime CreatedAt { get; set; }

        public TherapistProfile Therapist { get; set; }
        public User Client { get; set; }
        public Session Session { get; set; }
    }
}
