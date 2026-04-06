using EuroLeaguePlayerBuilder.Services.Models.Arenas;
using EuroLeaguePlayerBuilder.Services.Models.Games;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EuroLeaguePlayerBuilder.Services.Core.Interfaces
{
    public interface IGameService
    {
        Task<IEnumerable<GameDto>> GetAllGamesOrderedByTeamsNameAsync();

        Task<GameInputDto> GetGameInputDataAsync();

        Task CreateGameAsync(GameInputDto inputDto, string userId);

        Task<bool> GameExistsAsync(int id);

        Task<bool> IsGameOwnedByUserAsync(int gameId, string userId);

        Task UpdateGameScoreAsync(int gameId, int teamOneScore, int teamTwoScore);

        Task<IEnumerable<GameDto>> GetUserGamesAsync(string userId);

        Task<GameDeleteDto> GetGameForDeleteByIdAsync(int id);

        Task DeleteGameAsync(int id);

        Task<IEnumerable<AdminGameDto>> GetAllArenasForAdminAsync();
    }
}
