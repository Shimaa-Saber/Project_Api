using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project_Api.DTO;
using Project_Api.DTO.BookingSession;
using Project_Api.Interfaces;
using System.Security.Claims;

namespace Project_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SessionController : ControllerBase
    {

        private readonly ISlots _availabilityRepo;
        private readonly ILogger<SessionController> _logger;
        private readonly ISessions _sessionRepo;

        public SessionController(
            ISlots availabilityRepo,
            ILogger<SessionController> logger,
            ISessions sessionRepo
            
            )
        {
            _availabilityRepo = availabilityRepo;
            _logger = logger;
            _sessionRepo = sessionRepo;
        }



        [HttpGet("availability")]
        [ProducesResponseType(typeof(IEnumerable<AvailabilitySlotDto>), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetAvailability(
        [FromQuery] string therapistId,
        [FromQuery] DateTime? startDate = null,
        [FromQuery] DateTime? endDate = null,
        [FromQuery] string slotType = null)
        {
            try
            {
                var slots = await _availabilityRepo.GetAvailableSlotsAsync(
                    therapistId,
                    startDate,
                    endDate,
                    slotType);

                if (!slots.Any())
                    return NotFound("No available slots found");

                return Ok(slots);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetAvailability endpoint");
                return StatusCode(500, "Internal server error");
            }
        }



        [HttpPost("book")]
        public async Task<IActionResult> BookSession([FromForm][FromBody] BookSessionDto dto)
        {
            var result = await _sessionRepo.BookSessionAsync(dto);

            if (!result.IsSuccess)
                return BadRequest(result.Error);

            //return CreatedAtAction(
            // nameof(GetUpcomingSessions),
            //  new { id = result.Session!.Id },
            //  result.Session);
            return Ok(result.Session);
        }

        [HttpPut("{id}/cancel")]
        public async Task<IActionResult> CancelSession(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _sessionRepo.CancelSessionAsync(id, userId);

            return result.IsSuccess
                ? NoContent()
                : BadRequest(result.Error);
        }


        [HttpGet("upcoming")]
        public async Task<IActionResult> GetUpcomingSessions()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var sessions = await _sessionRepo.GetUpcomingSessionsAsync(userId);
            return Ok(sessions);
        }

        [HttpGet("history")]
        public async Task<IActionResult> GetSessionHistory()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var sessions = await _sessionRepo.GetSessionHistoryAsync(userId);
            return Ok(sessions);
        }


    }
}
