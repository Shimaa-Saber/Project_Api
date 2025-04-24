using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectApi.Models
{
    public class TherapistProfile
    {
        public int Id { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        public string Bio { get; set; }
        public int YearsOfExperience { get; set; }
        public decimal PricePerSession { get; set; }
        public string Timezone { get; set; }
        public bool SupportsVideo { get; set; }
        public bool SupportsAudio { get; set; }
        public bool SupportsText { get; set; }

        // Navigation
        public User User { get; set; }
        public ICollection<TherapistSpecialization> TherapistSpecializations { get; set; }
        public ICollection<AvailabilitySlot> AvailabilitySlots { get; set; }
        public ICollection<Session> Sessions { get; set; }
    }
}
