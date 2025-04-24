namespace ProjectApi.Models
{
    public class AvailabilitySlot
    {
        public int Id { get; set; }
        public int TherapistId { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public string SlotType { get; set; }
        public bool IsBooked { get; set; }

        public TherapistProfile Therapist { get; set; }
    }
}
