using Microsoft.AspNetCore.Identity;
using Project_Api.DTO;
using Project_Api.DTO.UserDtos;
using Project_Api.DTO.UserDtos.changePasswordDtos;
using Project_Api.Models;
using ProjectApi.Repositories;

namespace Project_Api.Interfaces
{
    public interface IAuth: IGenericRepository<ApplicationUser>
    {
        Task<IdentityResult> RegisterUserAsync(UserRegister userFromConsumer);
        Task<LoginResult> LoginUserAsync(Login userFromConsumer);
        Task<TherapistRegistrationResult> RegisterTherapistAsync(TherapistRegisterDto dto);
        Task<UserProfileResponse> GetUserProfileAsync(string userId);
        Task<UpdateProfileResult> UpdateUserProfileAsync(string userId, UpdateProfileRequest request);

        Task<ResponseDto> ChangePasswordAsync(ChangePasswordDto passwordDto);
        Task<ResponseDto> ForgotPasswordAsync(string email);
        Task<ResponseDto> ResetPasswordAsync(ResetPasswordDto passDto);
        Task<bool> UserExistsAsync(string userId);
    }

    public record UserProfileResponse(
    string Id,
    string Email,
    string UserName,
    string FullName,
    string PhoneNumber,
    DateTime? DateOfBirth,
    string Gender);

    public record UpdateProfileRequest(
        string? FullName,
        string? PhoneNumber,
        DateTime? DateOfBirth,
        string? Gender);

    public record UpdateProfileResult(bool Succeeded, IEnumerable<string>? Errors);

    public class LoginResult
    {
        public bool Success { get; set; }
        public string Token { get; set; }
        public DateTime Expiry { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }

    public class TherapistRegistrationResult
    {
        public bool Success { get; set; }
        public string UserId { get; set; }
        public string ProfileId { get; set; }
        public IEnumerable<string> Errors { get; set; }
        public string Message { get; set; }
    }
}
