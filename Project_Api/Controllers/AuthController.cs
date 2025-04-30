using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Project_Api.DTO.UserDtos;
using Project_Api.DTO.UserDtos.changePasswordDtos;
using Project_Api.Interfaces;
using Project_Api.Models;

using Project_Api.Services;
using ProjectApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Project_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration config;
        private readonly FileUploadService _fileService;
        private readonly IAuth _profileRepository;
        private readonly ILogger<AuthController> _logger;
        public AuthController(UserManager<ApplicationUser> userManager, IConfiguration config, SignInManager<ApplicationUser> signInManager
            , ApplicationDbContext context,IAuth auth, 
            FileUploadService fileUploadService,ILogger<AuthController> logger)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.config = config;
            _context = context;
            _fileService = fileUploadService;
            _profileRepository = auth;
            _logger = logger;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(
      [FromForm] UserRegister userFromConsumer
      )
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IdentityResult result = await _profileRepository.RegisterUserAsync(userFromConsumer);

            if (result.Succeeded)
            {
                return Ok("Account Create Success");
            }

            foreach (var item in result.Errors)
            {
                ModelState.AddModelError("", item.Description);
            }

            return BadRequest(ModelState);
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login(
      [FromForm] Login userFromConsumer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _profileRepository.LoginUserAsync(userFromConsumer);

            if (result.Success)
            {
                return Ok(new
                {
                    expired = result.Expiry,
                    token = result.Token
                });
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }

            return BadRequest(ModelState);
        }



        [HttpPost("register/therapist")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RegisterTherapist(
    [FromForm] TherapistRegisterDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _profileRepository.RegisterTherapistAsync(dto);

            if (!result.Success)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error);
                }
                return BadRequest(ModelState);
            }

            return Ok(new
            {
                UserId = result.UserId,
                ProfileId = result.ProfileId,
                Message = result.Message
            });
        }

        [HttpGet("GetProfile")]
     
        public async Task<IActionResult> GetProfile()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var profile = await _profileRepository.GetUserProfileAsync(userId);
            return profile == null ? NotFound() : Ok(profile);
        }


        [HttpPut("UpdateProfile")]
       
        public async Task<IActionResult> UpdateProfile([FromBody] UpdateProfileRequest request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _profileRepository.UpdateUserProfileAsync(userId, request);

            if (!result.Succeeded)
            {
                _logger.LogWarning("Profile update failed for {UserId}: {Errors}",
                    userId, string.Join(", ", result.Errors ?? Array.Empty<string>()));
                return BadRequest(result.Errors);
            }

            return NoContent();
        }


        [HttpPut("ChangePassword")]
        public async Task<IActionResult> ChangePasswordAsync(ChangePasswordDto password)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var response = await _profileRepository.ChangePasswordAsync(password);
            if (response.IsSucceeded)
                return Ok(response);
            return StatusCode(response.StatusCode);
        }


        [HttpPost("Forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _profileRepository.ForgotPasswordAsync(dto.Email);
            return response.IsSucceeded
                ? Ok(response)
                : StatusCode(response.StatusCode, response.model);
        }

        [HttpPut("ResetPassword")]
        public async Task<IActionResult> ResetPassswordAsync(ResetPasswordDto password)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var response = await _profileRepository.ResetPasswordAsync(password);
            if (response.IsSucceeded)
                return Ok(response);
            return StatusCode(response.StatusCode);
        }



















    }
}
