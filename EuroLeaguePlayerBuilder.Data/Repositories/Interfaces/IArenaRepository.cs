using EuroLeaguePlayerBuilder.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EuroLeaguePlayerBuilder.Data.Repositories.Interfaces
{
    public interface IArenaRepository
    {
        IQueryable<Arena> GetAllArenasNoTracking();

        IQueryable<Arena> GetAllArenas();

        Task<bool> AddArenaAsync(Arena arena);

        Task<Arena?> GetArenaByIdNoTrackingAsync(int id);

        Task UpdateArenaAsync(Arena selectedArena);

        Task DeleteArenaAsync(Arena selectedArena);
    }
}
