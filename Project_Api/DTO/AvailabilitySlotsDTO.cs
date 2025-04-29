namespace Project_Api.DTO
{
    public class AvailabilitySlotsDTO
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public string SlotType { get; set; }
        public DayOfWeek DayOfWeek { get; internal set; }

    }
}
