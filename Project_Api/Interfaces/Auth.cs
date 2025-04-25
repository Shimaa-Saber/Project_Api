using Project_Api.Models;
using ProjectApi.Repositories;

namespace Project_Api.Interfaces
{
    public interface Auth: IGenericRepository<ApplicationUser>
    {
        Task<UserProfileResponse> GetUserProfileAsync(string userId);
        Task<UpdateProfileResult> UpdateUserProfileAsync(string userId, UpdateProfileRequest request);
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
