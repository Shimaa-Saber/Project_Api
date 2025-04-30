using Microsoft.AspNetCore.Identity;
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

            modelBuilder.Entity<SpecializationType>().HasData(
                new SpecializationType { Id = 1, Name = "Cognitive Behavioral Therapy" },
                new SpecializationType { Id = 2, Name = "Dialectical Behavior Therapy" },
                new SpecializationType { Id = 3, Name = "Psychodynamic Therapy" },
                new SpecializationType { Id = 4, Name = "Humanistic Therapy" },
                new SpecializationType { Id = 5, Name = "Integrative Therapy" }
            );

            var adminUserId = Guid.NewGuid().ToString();
            var hasher = new PasswordHasher<ApplicationUser>();

            //modelBuilder.Entity<ApplicationUser>().HasData(
            //    new ApplicationUser
            //    {
            //        Id = adminUserId,
            //        UserName = "admin@example.com",
            //        NormalizedUserName = "ADMIN@EXAMPLE.COM",
            //        Email = "admin@example.com",
            //        NormalizedEmail = "ADMIN@EXAMPLE.COM",
            //        EmailConfirmed = true,
            //        PasswordHash = hasher.HashPassword(null, "Admin@1234!"),
            //        SecurityStamp = Guid.NewGuid().ToString(),
            //        ConcurrencyStamp = Guid.NewGuid().ToString(),
            //        Gender="Male",
            //        PhoneNumber = "0123456789",
            //        PhoneNumberConfirmed = true
            //    }
            //);


            //modelBuilder.Entity<IdentityUserRole<string>>().HasData(
            //    new IdentityUserRole<string> { UserId = adminUserId, RoleId = "1" }
            //);












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
           TherapistId = "943a7e8a-3164-4c88-be8b-58711088b81b",
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
           TherapistId = "943a7e8a-3164-4c88-be8b-58711088b81b",
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
           TherapistId = "943a7e8a-3164-4c88-be8b-58711088b81b",
           Date = DateTime.Today.AddDays(2),
           StartTime = new TimeSpan(10, 0, 0),
           EndTime = new TimeSpan(11, 0, 0),
           SlotType = "Video",
           IsAvailable = true,
           DayOfWeek = DateTime.Today.AddDays(2).DayOfWeek
       }
       ,

         new AvailabilitySlot
         {
             Id = 4,
             TherapistId = "943a7e8a-3164-4c88-be8b-58711088b81b",
             Date = DateTime.Today.AddDays(3),
             StartTime = new TimeSpan(10, 0, 0),
             EndTime = new TimeSpan(11, 0, 0),
             SlotType = "IsPerson",
             IsAvailable = true,
             DayOfWeek = DateTime.Today.AddDays(2).DayOfWeek
         },
           new AvailabilitySlot
           {
               Id = 5,
               TherapistId = "943a7e8a-3164-4c88-be8b-58711088b81b",
               Date = DateTime.Today.AddDays(4),
               StartTime = new TimeSpan(10, 0, 0),
               EndTime = new TimeSpan(10, 0, 0),
               SlotType = "Video",
               IsAvailable = true,
               DayOfWeek = DateTime.Today.AddDays(4).DayOfWeek
           }
   );





            modelBuilder.Entity<Notification>().HasData(
        new Notification
        {
            Id = 1,
            UserId = "cbb35a0f-83a4-4427-8647-bb9cab7aa8b2",
            Title = "Appointment Confirmed",
            Message = "Your session with Dr. Smith is confirmed for tomorrow at 2 PM",
            Type = "Appointment",
            // Metadata = "{\"AppointmentId\":101,\"Date\":\"2025-04-29\",\"Time\":\"14:00\"}",
            IsRead = false
        },
        new Notification
        {
            Id = 2,
            UserId = "cbb35a0f-83a4-4427-8647-bb9cab7aa8b2",
            Title = "New Message Received",
            Message = "You have 1 new message in your inbox",
            Type = "Message",
            // Metadata = "{\"Sender\":\"support@clinic.com\",\"Urgent\":true}",
            IsRead = true
        },
        new Notification
        {
            Id = 3,
            UserId = "cbb35a0f-83a4-4427-8647-bb9cab7aa8b2",
            Title = "Payment Processed",
            Message = "Your payment of $50.00 was completed successfully",
            Type = "Payment",
            // Metadata = "{\"Amount\":50.00,\"Method\":\"Credit Card\"}",
            IsRead = false
        },
        new Notification
        {
            Id = 4,
            UserId = "cbb35a0f-83a4-4427-8647-bb9cab7aa8b2",
            Title = "System Maintenance",
            Message = "Scheduled maintenance tonight from 1AM to 3AM",
            Type = "System",
            //    Metadata = "{\"Start\":\"01:00\",\"End\":\"03:00\",\"Timezone\":\"UTC\"}",
            IsRead = false
        }
    );




        }
    }
}
