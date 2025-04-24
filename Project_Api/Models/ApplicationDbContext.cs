using Microsoft.EntityFrameworkCore;

namespace ProjectApi.Models
{
    public class ApplicationDbContext : DbContext
    {
        //public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

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


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> option) : base(option)
        {

        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Payment>()
       .Property(p => p.Amount)
       .HasPrecision(18, 2); // 18 digits total, 2 decimal places

            modelBuilder.Entity<TherapistProfile>()
                .Property(t => t.PricePerSession)
                .HasPrecision(18, 2);

            //modelBuilder.Entity<TherapistSpecialization>()
            //    .Property(t => t.)
            //    .HasPrecision(3, 2); // e.g., 0.75, 1.00

            // Other configurations...
            modelBuilder.Entity<User>()
                .HasOne(u => u.TherapistProfile)
                .WithOne(t => t.User)
                .HasForeignKey<TherapistProfile>(t => t.UserId);
        }
    }
}
