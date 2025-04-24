using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectApi.Models
{
    public class AudioSessionDetail
    {
        public int Id { get; set; }
        [ForeignKey("Session")]
        public int SessionId { get; set; }
        public string CallUrl { get; set; }
        public string Platform { get; set; }
        public int Bitrate { get; set; }
        public int SampleRate { get; set; }

        public Session Session { get; set; }
    }
}
