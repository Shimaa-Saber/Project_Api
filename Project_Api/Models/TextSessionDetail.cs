using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectApi.Models
{
    public class TextSessionDetail
    {
        public int Id { get; set; }
        [ForeignKey("Session")]
        public int SessionId { get; set; }

        public int ChatId { get; set; }
        public string Transcript { get; set; }

        public Session Session { get; set; }
        public Chat Chat { get; set; }
    }
}
