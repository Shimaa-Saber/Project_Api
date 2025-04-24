namespace ProjectApi.Models
{
    public class VideoSessionDetail
    {
        public int SessionId { get; set; }
        public string MeetingUrl { get; set; }
        public string Platform { get; set; }
        public string Resolution { get; set; }
        public bool WasRecorded { get; set; }

        public Session Session { get; set; }
    }
}
