using Microsoft.EntityFrameworkCore;

namespace ProjectApi.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<TherapistProfile> TherapistProfiles { get; set; }
        public DbSet<SpecializationType> SpecializationTypes { get; set; }
        public DbSet<TherapistSpecialization> TherapistSpecializations { get; set; }
        public DbSet<AvailabilitySlot> AvailabilitySlots { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<VideoSessionDetail> VideoSessionDetails { get; set; }
        public DbSet<AudioSessionDetail> AudioSessionDetails { get; set; }
        public DbSet<TextSessionDetail> TextSessionDetails { get; set; }
        public DbSet<TherapistReview> TherapistReviews { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Payment> Payments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<VideoSessionDetail>().HasKey(v => v.SessionId);
            modelBuilder.Entity<AudioSessionDetail>().HasKey(a => a.SessionId);
            modelBuilder.Entity<TextSessionDetail>().HasKey(t => t.SessionId);

            modelBuilder.Entity<User>()
                .Property(u => u.Role)
                .HasConversion<string>();

            modelBuilder.Entity<Session>()
                .Property(s => s.Type)
                .HasConversion<string>();
        }
    }
}
