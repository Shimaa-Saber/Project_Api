using System;

namespace ProjectApi.Models
{
    public class TextSessionDetail
    {
        public int SessionId { get; set; }
        public int ChatId { get; set; }
        public string Transcript { get; set; }

        public Session Session { get; set; }
        public Chat Chat { get; set; }
    }
}
