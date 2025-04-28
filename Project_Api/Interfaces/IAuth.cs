using Project_Api.DTO;
using Project_Api.DTO.UserDtos.changePasswordDtos;
using Project_Api.Models;
using ProjectApi.Repositories;

namespace Project_Api.Interfaces
{
    public interface IAuth: IGenericRepository<ApplicationUser>
    {
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
}
