using EuroLeaguePlayerBuilder.Data.Models;
using EuroLeaguePlayerBuilder.Data.Repositories.Interfaces;
using EuroLeaguePlayerBuilder.Services.Core.Interfaces;
using EuroLeaguePlayerBuilder.Services.Models.Users;
using Microsoft.AspNetCore.Identity;
using static EuroLeaguePlayerBuilder.GCommon.ErrorMessages;

namespace EuroLeaguePlayerBuilder.Services.Core
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserService(IUserRepository userRepository, UserManager<ApplicationUser> userManager)
        {
            _userRepository = userRepository;
            _userManager = userManager;
        }

        public async Task<IEnumerable<UserDto>> GetAllUsersWithRolesAsync()
        {
            IEnumerable<ApplicationUser> users = await _userRepository.GetAllUsersAsync();

            List<UserDto> result = new List<UserDto>();

            foreach (var user in users)
            {
                IList<string> roles = await _userManager.GetRolesAsync(user);

                result.Add(new UserDto
                {
                    Id = user.Id,
                    Email = user.Email!,
                    Nickname = user.Nickname,
                    Role = roles.FirstOrDefault() ?? "No Role"
                });
            }

            return result;
        }

        public async Task PromoteToAdminAsync(string userId)
        {
            ApplicationUser? user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                throw new ArgumentException(UserNotFoundServiceError);
            }

            await _userManager.RemoveFromRoleAsync(user, "User");
            await _userManager.AddToRoleAsync(user, "Admin");
        }

        public async Task DemoteToUserAsync(string userId)
        {
            ApplicationUser? user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                throw new ArgumentException(UserNotFoundServiceError);
            }

            await _userManager.RemoveFromRoleAsync(user, "Admin");
            await _userManager.AddToRoleAsync(user, "User");
        }
    }
}
