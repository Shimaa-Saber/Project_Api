using Microsoft.EntityFrameworkCore;
using Project_Api.Interfaces;
using Project_Api.Reposatories;
using ProjectApi.Models;
using static Project_Api.Reposatories.TextSessionRepository;
using static Project_Api.Reposatories.TherapistProfileRepository;
namespace Project_Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddScoped<Chats, ChatRepository>();
            builder.Services.AddScoped<messages, MessageRepository>();
            builder.Services.AddScoped<Slots, AvailabilitySlotRepository>();
            builder.Services.AddScoped<VideoSession, VideoSessionRepository>();
            builder.Services.AddScoped<TextSession, TextSessionRepositoryS>();
            builder.Services.AddScoped<TherabistProfile, TherapistProfileRepositorys>();
            builder.Services.AddScoped<TherapistReviews, ReviewRepository>();
            builder.Services.AddScoped<Paymentt, PaymentRepository>();
            builder.Services.AddScoped<Notifications, NotificationRepository>();
















            builder.Services.AddDbContext<ApplicationDbContext>(options =>
             options.UseSqlServer(builder.Configuration.GetConnectionString("CS")));
            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
