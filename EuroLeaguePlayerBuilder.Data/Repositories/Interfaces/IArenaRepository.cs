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

        Task<bool> AddArenaAsync(Arena arena);
    }
}
