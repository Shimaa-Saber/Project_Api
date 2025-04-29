using System.ComponentModel.DataAnnotations.Schema;

namespace Project_Api.DTOs
{
    public class TherapistDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Bio { get; set; }
        public int YearsOfExperience { get; set; }
        public decimal PricePerSession { get; set; }

        public decimal NumberOfSessions { get; set; }   
        public string Timezone { get; set; }

        public string LicenseNumber { get; internal set; }

        public string LicenseCertificatePath { get; set; }
        public string ProfilePictureUrl { get; set; }
        public string[] Specialization {  get; set; }
    }
}
