using EuroLeaguePlayerBuilder.Data.Models;

namespace EuroLeaguePlayerBuilder.Data.Repositories.Interfaces
{
    public interface IArenaRepository
    {
        IQueryable<Arena> GetAllArenasNoTracking();

        IQueryable<Arena> GetAllArenas();

        Task<bool> AddArenaAsync(Arena arena);

        Task<Arena?> GetArenaByIdNoTrackingAsync(int id);

        Task UpdateArenaAsync(Arena selectedArena);

        Task DeleteArenaFromDbAsync(Arena selectedArena);

        IQueryable<Arena> GetAllArenasWithUserNoTracking();
    }
}
