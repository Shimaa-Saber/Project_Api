using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Project_Api.Models;

namespace ProjectApi.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        //public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

       // public DbSet<User> Users { get; set; }
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
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationRole>().HasData(
                new ApplicationRole { Id = "1", Name = "Admin", NormalizedName = "ADMIN" },
                new ApplicationRole { Id = "2", Name = "User", NormalizedName = "USER" },
                new ApplicationRole { Id = "3", Name = "Therapist", NormalizedName = "THERAPIST" }
            );
            modelBuilder.Entity<Payment>()
       .Property(p => p.Amount)
       .HasPrecision(18, 2); // 18 digits total, 2 decimal places

            modelBuilder.Entity<TherapistProfile>()
                .Property(t => t.PricePerSession)
                .HasPrecision(18, 2);

            modelBuilder.Entity<TherapistSpecialization>()
                    .HasOne(ts => ts.Therapist)
                    .WithMany(t => t.TherapistSpecializations)
                    .HasForeignKey(ts => ts.TherapistId)
                    .OnDelete(DeleteBehavior.Cascade);

            // Other configurations...
            //modelBuilder.Entity<User>()
            //    .HasOne(u => u.TherapistProfile)
            //    .WithOne(t => t.User)
            //    .HasForeignKey<TherapistProfile>(t => t.UserId);

            modelBuilder.Entity<AvailabilitySlot>().HasData(
       new AvailabilitySlot
       {
           Id = 1,
           TherapistId = "d6570062-9ae7-4109-84bb-19770cb70d08",
           Date = DateTime.Today.AddDays(1),
           StartTime = new TimeSpan(9, 0, 0),
           EndTime = new TimeSpan(10, 0, 0),
           SlotType = "Video",
           IsAvailable = true,
           DayOfWeek = DateTime.Today.AddDays(1).DayOfWeek
       },
       new AvailabilitySlot
       {
           Id = 2,
           TherapistId = "d6570062-9ae7-4109-84bb-19770cb70d08",
           Date = DateTime.Today.AddDays(1),
           StartTime = new TimeSpan(14, 0, 0),
           EndTime = new TimeSpan(15, 0, 0),
           SlotType = "InPerson",
           IsAvailable = true,
           DayOfWeek = DateTime.Today.AddDays(1).DayOfWeek
       },
       new AvailabilitySlot
       {
           Id = 3,
           TherapistId = "d6570062-9ae7-4109-84bb-19770cb70d08",
           Date = DateTime.Today.AddDays(2),
           StartTime = new TimeSpan(10, 0, 0),
           EndTime = new TimeSpan(11, 0, 0),
           SlotType = "Video",
           IsAvailable = true,
           DayOfWeek = DateTime.Today.AddDays(2).DayOfWeek
       }
   );








        }
    }
    }

