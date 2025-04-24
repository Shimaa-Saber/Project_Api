namespace ProjectApi.Models
{
    public class SpecializationType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Icon { get; set; }

        public ICollection<TherapistSpecialization> TherapistSpecializations { get; set; }
    }
}
