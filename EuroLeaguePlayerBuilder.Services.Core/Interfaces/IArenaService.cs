using EuroLeaguePlayerBuilder.Services.Models.Arenas;

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

        Task<IEnumerable<AdminArenaDto>> GetAllArenasForAdminAsync();

        Task<bool> IsArenaUserCreatedAsync(int arenaId);
    }
}
