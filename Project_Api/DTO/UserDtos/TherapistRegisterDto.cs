using System.ComponentModel.DataAnnotations;

namespace Project_Api.DTO.UserDtos
{
    public class TherapistRegisterDto
    {
        [Required] public string UserName { get; set; }
        [Required][EmailAddress] public string Email { get; set; }
        [Required] public string Password { get; set; }
        [Required][Compare("Password")] public string ConfirmPassword { get; set; }
        [Required] public string PhoneNumber { get; set; }


        [Required] public string FullName { get; set; }
        [Required(ErrorMessage = "Date of birth is required")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
        [Required] public string Gender { get; set; }


        [Required] public string Bio { get; set; }
        [Required] public int YearsOfExperience { get; set; }
        [Required] public decimal PricePerSession { get; set; }


        [Required][MinLength(1)] public List<int> SpecializationIds { get; set; }


        public List<AvailabilitySlotDto> DefaultAvailability { get; set; }

        [Required] public string LicenseNumber { get; set; }
        public IFormFile LicenseCertificate { get; set; }
        public IFormFile ProfilePicture { get; set; }
    }
}
