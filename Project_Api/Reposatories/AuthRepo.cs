using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Project_Api.DTO.UserDtos.changePasswordDtos;
using Project_Api.DTO;
using Project_Api.Interfaces;
using Project_Api.Models;
using ProjectApi.Models;
using Project_Api.DTO.UserDtos;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Project_Api.Services;

namespace Project_Api.Reposatories
{
    public class AuthRepo : IAuth
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailService _emailService;
        private readonly ILogger<AuthRepo> _logger;
        private readonly IConfiguration _config;
        private readonly FileUploadService _fileService;

        public AuthRepo(
            SignInManager<ApplicationUser> signInManager,
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            ILogger<AuthRepo> logger,
            IEmailService emailService,
            IConfiguration config,
            FileUploadService fileUploadService)
        {
            _context = context;
            _userManager = userManager;
            _logger = logger;
            _emailService = emailService;
            _signInManager = signInManager;
            _config = config;
            _fileService = fileUploadService;
        }

        public void Delete(ApplicationUser obj)
        {
            throw new NotImplementedException();
        }

        public List<ApplicationUser> GetAll()
        {
            throw new NotImplementedException();
        }

        public ApplicationUser GetById(int id)
        {
            throw new NotImplementedException();
        }


        public async Task<IdentityResult> RegisterUserAsync(UserRegister userFromConsumer)
        {
            ApplicationUser user = new ApplicationUser
            {
                FullName = userFromConsumer.UserName,
                Email = userFromConsumer.Email,
                PhoneNumber = userFromConsumer.Phone,
                UserName = userFromConsumer.UserName.Replace(" ", ""),
                PasswordHash = userFromConsumer.Password,
             //   PasswordHash = userFromConsumer.ConfirmPassword,
                DateOfBirth = userFromConsumer.DateOfBirth,
                Gender = userFromConsumer.Gender,
                IsVerified = true
            };

            IdentityResult result = await _userManager.CreateAsync(user, userFromConsumer.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "User");
                await _signInManager.SignInAsync(user, isPersistent: false);
            }

            return result;
        }





        public async Task<LoginResult> LoginUserAsync(Login userFromConsumer)
        {
            var user = await _userManager.FindByNameAsync(userFromConsumer.UserName);
            if (user == null)
            {
                return new LoginResult
                {
                    Success = false,
                    Errors = new[] { "Invalid Account" }
                };
            }

            var passwordValid = await _userManager.CheckPasswordAsync(user, userFromConsumer.Password);
            if (!passwordValid)
            {
                return new LoginResult
                {
                    Success = false,
                    Errors = new[] { "Invalid Account" }
                };
            }

            // Token generation (exact copy-paste from your original)
            string jti = Guid.NewGuid().ToString();
            var userRoles = await _userManager.GetRolesAsync(user);

            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(JwtRegisteredClaimNames.Jti, jti)
        };

            if (userRoles != null)
            {
                claims.AddRange(userRoles.Select(role => new Claim(ClaimTypes.Role, role)));
            }

            var signinKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Key"]));
            var signingCredentials = new SigningCredentials(signinKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["JWT:Iss"],
                audience: _config["JWT:Aud"],
                expires: DateTime.Now.AddHours(1),
                claims: claims,
                signingCredentials: signingCredentials
            );

            return new LoginResult
            {
                Success = true,
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiry = DateTime.Now.AddHours(1)
            };
        }



        public async Task<TherapistRegistrationResult> RegisterTherapistAsync(TherapistRegisterDto dto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                // 1. Validate specializations
                var specializationsExist = await _context.SpecializationTypes
                    .CountAsync(s => dto.SpecializationIds.Contains(s.Id)) == dto.SpecializationIds.Count;

                if (!specializationsExist)
                {
                    return new TherapistRegistrationResult
                    {
                        Success = false,
                        Errors = new[] { "One or more specializations are invalid" }
                    };
                }

                // 2. Create user
                var user = new ApplicationUser
                {
                    UserName = dto.UserName,
                    Email = dto.Email,
                    PhoneNumber = dto.PhoneNumber,
                    FullName = dto.FullName,
                    DateOfBirth = dto.DateOfBirth,
                    Gender = dto.Gender,
                    IsVerified = false
                };

                var result = await _userManager.CreateAsync(user, dto.Password);
                if (!result.Succeeded)
                {
                    return new TherapistRegistrationResult
                    {
                        Success = false,
                        Errors = result.Errors.Select(e => e.Description)
                    };
                }

                // 3. Add to Therapist role
                await _userManager.AddToRoleAsync(user, "Therapist");

                // 4. Create therapist profile
                var profile = new TherapistProfile
                {
                    Id = Guid.NewGuid().ToString("N"),
                    UserId = user.Id,
                    Bio = dto.Bio,
                    YearsOfExperience = dto.YearsOfExperience,
                    PricePerSession = dto.PricePerSession,
                    LicenseNumber = dto.LicenseNumber,
                    Status = VerificationStatus.Pending,
                    SupportsVideo = true,
                    SupportsAudio = true,
                    SupportsText = true
                };

                // 5. Handle file uploads
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
                    return new TherapistRegistrationResult
                    {
                        Success = false,
                        Errors = new[] { "Error processing files" }
                    };
                }

                // 6. Save profile
                await _context.TherapistProfiles.AddAsync(profile);
                await _context.SaveChangesAsync();

                // 7. Add specializations
                var therapistSpecializations = dto.SpecializationIds.Select(id =>
                    new TherapistSpecialization
                    {
                        TherapistId = profile.Id,
                        SpecializationId = id
                    });

                await _context.TherapistSpecializations.AddRangeAsync(therapistSpecializations);

                // 8. Set default availability
                if (dto.DefaultAvailability?.Any() == true)
                {
                    var slots = dto.DefaultAvailability.Select(a => new AvailabilitySlot
                    {
                        TherapistId = user.Id,
                        DayOfWeek = a.DayOfWeek,
                        StartTime = a.StartTime,
                        EndTime = a.EndTime
                    });

                    await _context.AvailabilitySlots.AddRangeAsync(slots);
                }

                // 9. Commit transaction
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return new TherapistRegistrationResult
                {
                    Success = true,
                    UserId = user.Id,
                    ProfileId = profile.Id,
                    Message = "Therapist registered successfully. Awaiting admin verification."
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Error registering therapist");
                return new TherapistRegistrationResult
                {
                    Success = false,
                    Errors = new[] { "An error occurred during registration" }
                };
            }
        }












        public async Task<UserProfileResponse> GetUserProfileAsync(string userId)
        {
            var user = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                _logger.LogWarning("User {UserId} not found", userId);
                return null;
            }

            return new UserProfileResponse(
                Id: user.Id,
                Email: user.Email,
                UserName: user.UserName,
                FullName: user.FullName,
                PhoneNumber: user.PhoneNumber,
                DateOfBirth: user.DateOfBirth,
                Gender: user.Gender);
        }

        public void insert(ApplicationUser obj)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void Update(ApplicationUser obj)
        {
            throw new NotImplementedException();
        }

        public async Task<UpdateProfileResult> UpdateUserProfileAsync(string userId, UpdateProfileRequest request)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return new UpdateProfileResult(false, new[] { "User not found" });
            }

          
            user.FullName = request.FullName ?? user.FullName;
            user.PhoneNumber = request.PhoneNumber ?? user.PhoneNumber;
            user.DateOfBirth = request.DateOfBirth ?? user.DateOfBirth;
            user.Gender = request.Gender ?? user.Gender;

            var result = await _userManager.UpdateAsync(user);
            return new UpdateProfileResult(
                result.Succeeded,
                result.Errors?.Select(e => e.Description));
        }

        public async Task<bool> UserExistsAsync(string userId)
        {
            return await _userManager.Users
                .AnyAsync(u => u.Id == userId);
        }


        public async Task<ResponseDto> ChangePasswordAsync(ChangePasswordDto passwordDto)
        {
            var user = await _userManager.FindByEmailAsync(passwordDto.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, passwordDto.CurrentPassword))
            {
                return new ResponseDto
                {
                    Message = "User not found!!",
                    IsSucceeded = false,
                    StatusCode = 400
                };
            }
            var result = await _userManager.ChangePasswordAsync(user, passwordDto.CurrentPassword, passwordDto.NewPassword);
            if (!result.Succeeded)
            {
                return new ResponseDto
                {

                    Message = "Failed to change password , try agin",
                    IsSucceeded = false,
                    StatusCode = 400
                };
            }
            return new ResponseDto
            {
                Message = "Password has changed successfully",
                IsSucceeded = true,
                StatusCode = 200
            };
        }


        public async Task<ResponseDto> ForgotPasswordAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return new ResponseDto
                {
                    Message = "User not found!",
                    IsSucceeded = false,
                    StatusCode = 404
                };
            }
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            string subject = "Password Reset Request";
            string message = $"Please use the following token to reset your password: {token}";
            await _emailService.sendEmailAsync(email, subject, message);
            return new ResponseDto
            {
                Message = "Password reset link sent successfully!",
                IsSucceeded = true,
                StatusCode = 200
            };
        }


        public async Task<ResponseDto> ResetPasswordAsync(ResetPasswordDto passDto)
        {
            var user = await _userManager.FindByEmailAsync(passDto.Email);
            if (user == null)
                return new ResponseDto
                {
                    Message = "User not found!!",
                    IsSucceeded = false,
                    StatusCode = 400
                };


            var result = await _userManager.ResetPasswordAsync(user, passDto.Token, passDto.NewPassword);
            if (!result.Succeeded)
            {
                return new ResponseDto
                {

                    Message = "Failed to reset password , try agin",
                    IsSucceeded = false,
                    StatusCode = 400
                };
            }
            return new ResponseDto
            {
                Message = "Passwored reset successfully ",
                IsSucceeded = true,
                StatusCode = 200
            };
        }
    }

}

