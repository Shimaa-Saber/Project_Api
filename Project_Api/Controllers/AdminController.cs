using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project_Api.Models;
using ProjectApi.Models;

namespace Project_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public AdminController(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        [HttpGet("therapists/pending")]
        public ActionResult<GeneralResponse> GetPendingTherapists()
        {
            var pendingTherapists = _context.TherapistProfiles
                .Include(t => t.User)
                .Where(t => t.Status == VerificationStatus.Pending)
                .Select(t => new
                {
                    t.Id,
                    t.User.FullName,
                    t.User.Email,
                    t.Status,
                    t.LicenseNumber,
                    t.ProfilePictureUrl
                }).ToList();
            if(pendingTherapists.Count() == 0 )
                return new GeneralResponse { IsPass = true , Data = "There is No Therapists are Pending"};
           
            return new GeneralResponse
            {
                IsPass = true,
                Data = pendingTherapists
            };
        }

        [HttpPut("users/{id}/ban")]
        public async Task<ActionResult<GeneralResponse>> BanOrUnbanUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return new GeneralResponse
                {
                    IsPass = false,
                    Data = "User not found"
                };
            }

            if (user.LockoutEnabled && user.LockoutEnd > DateTime.UtcNow)
            {
                user.LockoutEnd = DateTime.UtcNow; // Unban
            }
            else
            {
                user.LockoutEnabled = true;
                user.LockoutEnd = DateTime.UtcNow.AddYears(100); // Ban
            }

            await _userManager.UpdateAsync(user);

            return new GeneralResponse
            {
                IsPass = true,
                Data = "User ban status updated"
            };
        }

        [HttpGet("reports/sessions")]
        public ActionResult<GeneralResponse> GetSessionReport()
        {
            var sessions = _context.Sessions;

            if(sessions.Count() == 0)
            {
                return new GeneralResponse { IsPass = true, Data = "There is No Session yet" };
            }
            var report = new
            {
                Total = sessions.Count(),
                Completed = sessions.Count(s => s.Status == SessionStatus.Completed),
                Cancelled = sessions.Count(s => s.Status == SessionStatus.Cancelled),
                Pending = sessions.Count(s => s.Status == SessionStatus.Pending),
                Confirmed = sessions.Count(s => s.Status == SessionStatus.Confirmed)
            };

            return new GeneralResponse
            {
                IsPass = true,
                Data = report
            };
        }

        [HttpDelete("reviews/{id}")]
        public ActionResult<GeneralResponse> DeleteReview(int id)
        {
            var review = _context.TherapistReviews.Find(id);
            if (review == null)
            {
                return new GeneralResponse
                {
                    IsPass = false,
                    Data = "Review not found"
                };
            }

            _context.TherapistReviews.Remove(review);
            _context.SaveChanges();

            return new GeneralResponse
            {
                IsPass = true,
                Data = "Review deleted"
            };
        }
    }
}
