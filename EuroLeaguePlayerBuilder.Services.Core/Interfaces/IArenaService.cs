using EuroLeaguePlayerBuilder.Services.Models.Arenas;
using EuroLeaguePlayerBuilder.Services.Models.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EuroLeaguePlayerBuilder.Services.Core.Interfaces
{
    public interface IArenaService
    {
        Task<IEnumerable<ArenaDto>> GetAllArenasOrderedByNameAsync();

        Task CreateArenaAsync(ArenaInputDto inputDto, string userId, string wwwRootPath);

        Task<IEnumerable<ArenaDto>> GetUserArenas(string userId);

        Task<ArenaInputDto> GetArenaInputModelWithLoadedDataAsync(int id);

        Task<bool> IsArenaOwnedByUserAsync(int arenaId, string userId);

        Task<bool> ArenaExistsAsync(int id);

        Task EditArenaAsync(int id, ArenaInputDto inputDto, string wwwRootPath);

        Task<DeleteArenaDto> GetArenaForDeleteByIdAsync(int id);

        Task DeleteArenaAsync(int id);
    }
}
