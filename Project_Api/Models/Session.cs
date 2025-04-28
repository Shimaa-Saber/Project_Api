using Project_Api.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectApi.Models
{
    public enum SessionType
    {
        [Description("Video Call")]
        Video = 0,

        [Description("Voice Call")]
        Audio = 1,

        [Description("Text Chat")]
        Text = 2
    }
    public enum SessionStatus { Pending, Confirmed, Cancelled, Completed }

    public class Session
    {
        public int Id { get; set; }
        [ForeignKey("Client")]
        public string ClientId { get; set; }
        [ForeignKey("Therapist")]
        public string TherapistId { get; set; }
        [ForeignKey("AvailabilitySlot")]
        public int AvailabilitySlotId { get; set; }
        public SessionType Type { get; set; }
        public SessionStatus Status { get; set; }
        public string? Notes { get; set; }
        public DateTime CreatedAt { get; set; }

        public ApplicationUser? Client { get; set; }
        public ApplicationUser ?Therapist { get; set; }
        public AvailabilitySlot? AvailabilitySlot { get; set; }

        public VideoSessionDetail ?VideoSessionDetail { get; set; }
        public AudioSessionDetail? AudioSessionDetail { get; set; }
        public TextSessionDetail ?TextSessionDetail { get; set; }
        public ICollection<TherapistReview>? Reviews { get; set; }
        public Payment? Payment { get; set; }
    }
}
