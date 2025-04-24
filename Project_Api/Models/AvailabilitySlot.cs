using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectApi.Models
{
    public class AvailabilitySlot
    {
        public int Id { get; set; }
        [ForeignKey("Therapist")]
        public int TherapistId { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public string SlotType { get; set; }
        public bool IsBooked { get; set; }

        public User Therapist { get; set; }
    }
}
