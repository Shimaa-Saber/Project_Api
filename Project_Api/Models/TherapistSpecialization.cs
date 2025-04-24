using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectApi.Models
{
    public class TherapistSpecialization
    {
        public int Id { get; set; }
        [ForeignKey("Therapist")]
        public int TherapistId { get; set; }
        [ForeignKey("Specialization")]
        public int SpecializationId { get; set; }

        public TherapistProfile Therapist { get; set; }
        public SpecializationType Specialization { get; set; }
    }
}
