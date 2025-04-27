using System.ComponentModel.DataAnnotations;

namespace Project_Api.DTO
{
    public class AvailabilitySlotDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Date of birth is required")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public string SlotType { get; set; }
    }
}
