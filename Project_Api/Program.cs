using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Project_Api.Hubs;
using Project_Api.Interfaces;
using Project_Api.Models;
using Project_Api.Reposatories;
using Project_Api.Services;
using ProjectApi.Models;
using System.Text;
using static Project_Api.Reposatories.TextSessionRepository;
using static Project_Api.Reposatories.TherapistProfileRepository;
namespace Project_Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));






            builder.Services.AddScoped<IChats, ChatRepository>();
            builder.Services.AddScoped<Imessages, MessageRepository>();
            builder.Services.AddScoped<ISlots, AvailabilitySlotRepository>();
            builder.Services.AddScoped<IVideoSession, VideoSessionRepository>();
            builder.Services.AddScoped<ITextSession, TextSessionRepositoryS>();
            builder.Services.AddScoped<ITherabistProfile, TherapistProfileRepositorys>();
            builder.Services.AddScoped<ITherapistReviews, ReviewRepository>();
            builder.Services.AddScoped<IPaymentt, PaymentRepository>();
            builder.Services.AddScoped<INotifications, NotificationRepository>();
            builder.Services.AddScoped<IAuth, AuthRepo>();
            builder.Services.AddScoped<FileUploadService>();
            builder.Services.AddScoped<ISessions, SessionRepository>();
            builder.Services.AddScoped<IEmailService, EmailService>();
            builder.Services.AddScoped<IAudioSession, AudioRepo>();


            builder.Services.AddHttpContextAccessor();
            builder.Services.AddSignalR();
            builder.Services.AddSingleton<IUserConnectionTracker, UserConnectionTracker>();

            builder.Services.AddSignalR(options => {
                options.EnableDetailedErrors = true;
            });





            builder.Services.AddDbContext<ApplicationDbContext>(options =>
             options.UseSqlServer(builder.Configuration.GetConnectionString("CS")));


            builder.Services.AddIdentity<ApplicationUser, ApplicationRole>()
                           .AddEntityFrameworkStores<ApplicationDbContext>()
                           .AddDefaultTokenProviders();


            builder.Services.AddControllers().ConfigureApiBehaviorOptions(options =>
            options.SuppressModelStateInvalidFilter = true );


            builder.Services.AddAuthentication(options => {
                options.DefaultAuthenticateScheme =
                    JwtBearerDefaults.AuthenticationScheme;//check using jwt toke
                options.DefaultChallengeScheme =
                    JwtBearerDefaults.AuthenticationScheme; //redrect response in case not found cookie | token
                options.DefaultScheme =
                    JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new()
                {
                    ValidateIssuer = true,
                    ValidIssuer = builder.Configuration["JWT:Iss"],//proivder
                    ValidateAudience = true,
                    ValidAudience = builder.Configuration["JWT:Aud"],
                    IssuerSigningKey = new SymmetricSecurityKey
                    (Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]))
                };
            });




            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            // builder.Services.AddSwaggerGen();
            builder.Services.AddSwaggerGen(swagger =>
            {
                //This is to generate the Default UI of Swagger Documentation    
                swagger.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "ASP.NET 5 Web API",
                    Description = " ITI Projrcy",
                  
                   
                });

               
                // To Enable authorization using Swagger (JWT)    
                swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter 'Bearer' [space] and then your valid token in the text input below.\r\n\r\nExample: \"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9\"",
                });
                swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                    new OpenApiSecurityScheme
                    {
                    Reference = new OpenApiReference
                    {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                    }
                    },
                    new string[] {}
                    }
                    });
            });

            var app = builder.Build();
          
            app.UseStaticFiles();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseAuthentication();

            app.UseAuthorization();


            app.MapControllers();
            app.MapHub<NotificationHub>("/notificationHub");

            app.Run();
        }
    }
}
