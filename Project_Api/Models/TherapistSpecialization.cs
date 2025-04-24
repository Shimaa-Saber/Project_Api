namespace ProjectApi.Models
{
    public class TherapistSpecialization
    {
        public int Id { get; set; }
        public int TherapistId { get; set; }
        public int SpecializationId { get; set; }

        public TherapistProfile Therapist { get; set; }
        public SpecializationType Specialization { get; set; }
    }
}
