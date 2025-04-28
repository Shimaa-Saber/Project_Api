using ProjectApi.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Project_Api.DTO.BookingSession
{
    public class BookSessionDto
    {
        [Required] public string ClientId { get; set; }
        [Required] public string TherapistId { get; set; }
        [Required] public int AvailabilitySlotId { get; set; }
        
        [JsonConverter(typeof(JsonStringEnumConverter))]
        [Required] public SessionType  SessionType { get; set; }
        public string Notes { get; set; }
    }
}
