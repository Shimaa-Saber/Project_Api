using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectApi.Models
{
    public enum SessionType { Video, Audio, Text }
    public enum SessionStatus { Pending, Confirmed, Cancelled, Completed }

    public class Session
    {
        public int Id { get; set; }
        [ForeignKey("Client")]
        public int ClientId { get; set; }
        [ForeignKey("Therapist")]
        public int TherapistId { get; set; }
        [ForeignKey("AvailabilitySlot")]
        public int AvailabilitySlotId { get; set; }
        public SessionType Type { get; set; }
        public SessionStatus Status { get; set; }
        public string Notes { get; set; }
        public DateTime CreatedAt { get; set; }

        public User Client { get; set; }
        public TherapistProfile Therapist { get; set; }
        public AvailabilitySlot AvailabilitySlot { get; set; }

        public VideoSessionDetail VideoSessionDetail { get; set; }
        public AudioSessionDetail AudioSessionDetail { get; set; }
        public TextSessionDetail TextSessionDetail { get; set; }
        public ICollection<TherapistReview> Reviews { get; set; }
        public Payment Payment { get; set; }
    }
}
