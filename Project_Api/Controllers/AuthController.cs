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

        [HttpPost("register")]//api/accoutn/register
        public async Task<IActionResult> Register([FromForm] UserRegister userFromConsumer)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser();


                user.FullName = userFromConsumer.UserName;
                user.Email = userFromConsumer.Email;
                user.PhoneNumber = userFromConsumer.Phone;
                user.UserName = userFromConsumer.UserName.Replace(" ", "");
                user.PasswordHash = userFromConsumer.Password;
                user.PasswordHash = userFromConsumer.ConfirmPassword;
                user.DateOfBirth = userFromConsumer.DateOfBirth;
                user.Gender = userFromConsumer.Gender;
                user.IsVerified = true;

                
                IdentityResult result = await userManager.CreateAsync(user, userFromConsumer.Password);
                if (result.Succeeded)
                {

                    await userManager.AddToRoleAsync(user, "User");
                    await signInManager.SignInAsync(user, isPersistent: false);
                    //create token
                    return Ok("Account Create Success");
                }
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }
            return BadRequest(ModelState);
        }

        [HttpPost("login")]//api/accuont/login
        public async Task<IActionResult> Login([FromForm] Login userFromConsumer)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = await userManager.FindByNameAsync(userFromConsumer.UserName);
                if (user != null)
                {
                    bool found = await userManager.CheckPasswordAsync(user, userFromConsumer.Password);
                    if (found)
                    {
                        #region Create Token
                        string jti = Guid.NewGuid().ToString();
                        var userRoles = await userManager.GetRolesAsync(user);


                        List<Claim> claim = new List<Claim>();
                        claim.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
                        claim.Add(new Claim(ClaimTypes.Name, user.UserName));
                        claim.Add(new Claim(JwtRegisteredClaimNames.Jti, jti));//optional
                        if (userRoles != null)
                        {
                            foreach (var role in userRoles)
                            {
                                claim.Add(new Claim(ClaimTypes.Role, role));
                            }
                        }
                        //-----------------------------------------------
                        SymmetricSecurityKey signinKey =
                            new(Encoding.UTF8.GetBytes(config["JWT:Key"]));//hjhjhjhjh

                        SigningCredentials signingCredentials =
                            new SigningCredentials(signinKey, SecurityAlgorithms.HmacSha256);

                        JwtSecurityToken myToken = new JwtSecurityToken(
                            issuer: config["JWT:Iss"],//url service provider
                            audience: config["JWT:Aud"],//url service consumer
                            expires: DateTime.Now.AddHours(1),
                            claims: claim,
                            signingCredentials: signingCredentials
                            );//

                        //copact jhjh.kjk.lkl
                        return Ok(new
                        {
                            expired = DateTime.Now.AddHours(1),
                            token = new JwtSecurityTokenHandler().WriteToken(myToken)
                        });
                        #endregion
                    }
                }
                ModelState.AddModelError("", "Invalid Account");
            }
            return BadRequest(ModelState);
        }


        [HttpPost("register/therapist")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RegisterTherapist([FromForm] TherapistRegisterDto dto)
        {
            // 1. Validate input
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // 2. Check if specializations exist
            var specializationsExist = await _context.SpecializationTypes
                .CountAsync(s => dto.SpecializationIds.Contains(s.Id)) == dto.SpecializationIds.Count;

            if (!specializationsExist)
            {
                return BadRequest("One or more specializations are invalid");
            }

            // 3. Create user
            var user = new ApplicationUser
            {
                UserName = dto.UserName,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                FullName = dto.FullName,
                DateOfBirth = dto.DateOfBirth,
                Gender = dto.Gender,
                IsVerified = false // Admin must verify
            };

            var result = await userManager.CreateAsync(user, dto.Password);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            // 4. Add to Therapist role
            await userManager.AddToRoleAsync(user, "Therapist");

            // 5. Create therapist profile with explicit ID
            var profile = new TherapistProfile
            {
                Id = Guid.NewGuid().ToString("N"),
                UserId = user.Id,
                Bio = dto.Bio,
                YearsOfExperience = dto.YearsOfExperience,
                PricePerSession = dto.PricePerSession,
                LicenseNumber = dto.LicenseNumber,
                Status = VerificationStatus.Pending,
                SupportsVideo = true, // Set based on your requirements
                SupportsAudio = true,
                SupportsText = true
            };

            // 6. Handle file uploads
            try
            {
                if (dto.LicenseCertificate != null)
                {
                    profile.LicenseCertificatePath = await _fileService.SaveFileAsync(dto.LicenseCertificate);
                }

                if (dto.ProfilePicture != null)
                {
                    profile.ProfilePictureUrl = await _fileService.SaveFileAsync(dto.ProfilePicture);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error saving therapist files");
                return StatusCode(500, "Error processing files");
            }

            // 7. Save profile first
            await _context.TherapistProfiles.AddAsync(profile);
            await _context.SaveChangesAsync();

            // 8. Add specializations after profile is saved
            var therapistSpecializations = dto.SpecializationIds.Select(id =>
                new TherapistSpecialization
                {
                    TherapistId = profile.Id, // Use profile.Id here
                    SpecializationId = id
                });

            await _context.TherapistSpecializations.AddRangeAsync(therapistSpecializations);

            // 9. Set default availability if provided
            if (dto.DefaultAvailability?.Any() == true)
            {
                var slots = dto.DefaultAvailability.Select(a => new AvailabilitySlot
                {
                    TherapistId = user.Id, // This should match your AvailabilitySlot model
                    DayOfWeek = a.DayOfWeek,
                    StartTime = a.StartTime,
                    EndTime = a.EndTime
                });

                await _context.AvailabilitySlots.AddRangeAsync(slots);
            }

            // 10. Save all remaining changes
            await _context.SaveChangesAsync();

            // 11. Return success response
            return Ok(new
            {
                UserId = user.Id,
                ProfileId = profile.Id,
                Message = "Therapist registered successfully. Awaiting admin verification."
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
