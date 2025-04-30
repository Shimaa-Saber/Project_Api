using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectApi.Models;
using Microsoft.EntityFrameworkCore;

namespace Project_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
      
        public ReviewsController(ApplicationDbContext context)
        {
            _context = context;
        }

        
        [HttpPost]
        public async Task<IActionResult> SubmitReview(TherapistReview review)
        {
            review.CreatedAt = DateTime.UtcNow;
            review.IsVerified = false; 
            _context.TherapistReviews.Add(review);
            await _context.SaveChangesAsync();
            return Ok(review);
        }

        
        [HttpPut("{id}/response")]
        public async Task<IActionResult> RespondToReview(int id, [FromBody] string therapistResponse)
        {
            var review = await _context.TherapistReviews.FindAsync(id);
            if (review == null)
                return NotFound();

           
            review.Content += "\n\nTherapist Response: " + therapistResponse;
            await _context.SaveChangesAsync();
            return Ok(review);
        }

        [HttpGet("me")]
        public async Task<ActionResult<IEnumerable<TherapistReview>>> GetMyReviews()
        {
           
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest("Invalid user ID in token.");
            }

           
            var reviews = await _context.TherapistReviews
                .Where(r =>  r.TherapistId == userId)
                .ToListAsync();

            return Ok(reviews);
        }
       
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteReview(int id)
        {
            var review = await _context.TherapistReviews.FindAsync(id);
            if (review == null)
                return NotFound();

            _context.TherapistReviews.Remove(review);
            await _context.SaveChangesAsync();
            return NoContent();
        }

    }
}
