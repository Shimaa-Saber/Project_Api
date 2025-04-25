using Project_Api.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectApi.Models
{
    public class AvailabilitySlot
    {
        public int Id { get; set; }
        [ForeignKey("Therapist")]
        public string TherapistId { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public string SlotType { get; set; }
        public bool IsBooked { get; set; }

        public ApplicationUser Therapist { get; set; }
        public DayOfWeek DayOfWeek { get; internal set; }
    }
}
