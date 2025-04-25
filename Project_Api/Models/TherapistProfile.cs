using Project_Api.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectApi.Models
{
    public class TherapistProfile
    {
        public string Id { get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; }
        public string Bio { get; set; }
        public int YearsOfExperience { get; set; }
        public decimal PricePerSession { get; set; }
        public string Timezone { get; set; }
        public bool SupportsVideo { get; set; }
        public bool SupportsAudio { get; set; }
        public bool SupportsText { get; set; }
        public string LicenseNumber { get; internal set; }

        public string LicenseCertificatePath { get; set; }
        public string ProfilePictureUrl { get; set; }

        public VerificationStatus Status { get; set; } = VerificationStatus.Pending;

        // Navigation
        public ApplicationUser User { get; set; }
        public ICollection<TherapistSpecialization> TherapistSpecializations { get; set; }
        public ICollection<AvailabilitySlot> AvailabilitySlots { get; set; }
        public ICollection<Session> Sessions { get; set; }
     
    }
}


public enum VerificationStatus { Pending, Approved, Rejected }
