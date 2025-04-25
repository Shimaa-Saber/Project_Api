using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Project_Api.Interfaces;
using Project_Api.Models;
using ProjectApi.Models;

namespace Project_Api.Reposatories
{
    public class AuthRepo : Auth
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<AuthRepo> _logger;

        public AuthRepo(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            ILogger<AuthRepo> logger)
        {
            _context = context;
            _userManager = userManager;
            _logger = logger;
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
    }

}

