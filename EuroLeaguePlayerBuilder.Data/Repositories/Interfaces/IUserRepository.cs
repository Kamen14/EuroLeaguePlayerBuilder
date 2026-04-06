using EuroLeaguePlayerBuilder.Data.Models;

namespace EuroLeaguePlayerBuilder.Data.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<ApplicationUser>> GetAllUsersAsync();
    }
}
