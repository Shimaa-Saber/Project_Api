using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project_Api.DTO;
using Project_Api.Interfaces;

namespace Project_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentBookingController : ControllerBase
    {

        private readonly Slots _availabilityRepo;
        private readonly ILogger<AppointmentBookingController> _logger;

        public AppointmentBookingController(
            Slots availabilityRepo,
            ILogger<AppointmentBookingController> logger)
        {
            _availabilityRepo = availabilityRepo;
            _logger = logger;
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
    }
}
