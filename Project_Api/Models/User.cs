namespace ProjectApi.Models
{
    public enum UserRole
    {
        Client,
        Therapist,
        Admin
    }
    public class User
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Phone { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public UserRole Role { get; set; }
        public bool IsVerified { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Navigation properties
        public TherapistProfile TherapistProfile { get; set; }
        public ICollection<Notification> Notifications { get; set; }
        public ICollection<Payment> Payments { get; set; }
    }
}
