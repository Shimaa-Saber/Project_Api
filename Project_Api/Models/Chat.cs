namespace ProjectApi.Models
{
    public class Chat
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public int TherapistId { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastMessageAt { get; set; }

        public User Client { get; set; }
        public TherapistProfile Therapist { get; set; }
        public ICollection<Message> Messages { get; set; }
    }
}
