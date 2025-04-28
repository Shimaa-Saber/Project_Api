using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Project_Api.DTO.UserDtos.changePasswordDtos;
using Project_Api.DTO;
using Project_Api.Interfaces;
using Project_Api.Models;
using ProjectApi.Models;

namespace Project_Api.Reposatories
{
    public class AuthRepo : IAuth
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailService _emailService;
        private readonly ILogger<AuthRepo> _logger;

        public AuthRepo(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            ILogger<AuthRepo> logger,
            IEmailService emailService)
        {
            _context = context;
            _userManager = userManager;
            _logger = logger;
            _emailService = emailService;
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

