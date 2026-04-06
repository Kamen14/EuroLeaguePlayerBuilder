using EuroLeaguePlayerBuilder.Data.Models;

namespace EuroLeaguePlayerBuilder.Data.Repositories.Interfaces
{
    public interface IGameRepository
    {
        IQueryable<Game> GetAllGamesNoTracking();

        IQueryable<Game> GetAllGames();

        Task<bool> AddGameAsync(Game game);

        Task UpdateGameAsync(Game selectedGame);

        Task DeleteGameFromDbAsync(Game selectedGame);

        IQueryable<Game> GetAllGamesWithDetailsNoTracking();

        IQueryable<Game> GetAllGamesWithUserNoTracking();
    }
}
