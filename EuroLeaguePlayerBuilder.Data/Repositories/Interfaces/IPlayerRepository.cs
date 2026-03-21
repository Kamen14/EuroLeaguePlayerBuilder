using EuroLeaguePlayerBuilder.Data.Models;

namespace EuroLeaguePlayerBuilder.Data.Repositories.Interfaces
{
    public interface IPlayerRepository
    {
        IQueryable<Player> GetAllPlayers();
        IQueryable<Player> GetAllPlayersNoTracking();

        Task<Player?> GetPlayerWithTeamByIdNoTrackingAsync(int id);

        IQueryable<Team> GetAllTeamsNoTracking();

        Task<bool> AddPlayerAsync(Player player);

        Task UpdatePlayerAsync(Player selectedPlayer);

        Task DeletePlayerAsync(Player selectedPlayer);
    }
}
