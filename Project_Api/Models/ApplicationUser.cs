using Microsoft.AspNetCore.Identity;
using ProjectApi.Models;

namespace Project_Api.Models
{
    public class ApplicationUser:IdentityUser
    {
        public string? FullName { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
           //public string ConfirmPassword { get; set; }

        public bool? IsVerified { get; set; }

        public TherapistProfile TherapistProfile { get; set; }
        public ICollection<Notification> Notifications { get; set; }
        public ICollection<Payment> Payments { get; set; }

    }
}
