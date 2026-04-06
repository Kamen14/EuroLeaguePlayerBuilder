using EuroLeaguePlayerBuilder.Services.Models.Users;

namespace EuroLeaguePlayerBuilder.Services.Core.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAllUsersWithRolesAsync();
        Task PromoteToAdminAsync(string userId);
        Task DemoteToUserAsync(string userId);
    }
}
