using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project_Api.DTO.Review;
using Project_Api.Interfaces;
using Project_Api.Reposatories;
using ProjectApi.Models;
using System;
using System.Security.Claims;

namespace Project_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly ITherapistReviews _reviewRepository;
        private readonly ILogger<ReviewsController> _logger;


        public ReviewsController(ITherapistReviews reviewRepository, ILogger<ReviewsController> logger)
        {
          
            _logger = logger;
            _reviewRepository = reviewRepository;
        }


        [HttpPost]
        [Authorize(Roles = "Client")]
        public async Task<IActionResult> CreateReview([FromBody] CreateReviewDto dto)
        {
            var clientId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var review = await _reviewRepository.CreateReviewAsync(clientId, dto);

            return Ok(review);
        }


    [HttpPut("{id}/response")]
    [Authorize(Roles = "Therapist")]
        public async Task<IActionResult> AddResponse(
        int id,
        [FromBody] ReviewResponseDto dto)
        {
            var therapistId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var review = await _reviewRepository.AddTherapistResponseAsync(id, therapistId, dto);

            return review == null ? NotFound() : Ok(review);
        }

        [HttpGet("me")]
        public async Task<IActionResult> GetMyReviews()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var reviews = await _reviewRepository.GetUserReviewsAsync(userId);

            return Ok(reviews);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteReview(int id)
        {
            var success = await _reviewRepository.DeleteReviewAsync(id, isAdmin: true);
            return success ? NoContent() : NotFound();
        }

    }
}
