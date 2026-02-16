using EuroLeaguePlayerBuilder.ViewModels.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EuroLeaguePlayerBuilder.Services.Core.Interfaces
{
    public interface IPlayerService 
    {
        Task<IEnumerable<PlayerViewModel>> GetAllPlayersOrderedByNameAsync();

        Task<PlayerDetailsViewModel> GetPlayerDetailsByIdAsync(int id);

        Task<PlayerInputModel> GetPlayerInputModelWithLoadedTeamsAsync();

        Task<IEnumerable<CreatePlayerTeamViewModel>> LoadTeamsDropdownAsync();

        Task CreatePlayerAsync(PlayerInputModel inputModel, string userId);

        Task <PlayerInputModel> GetPlayerInputModelWithLoadedTeamsAndPlayerDataAsync(int id);

        Task<bool> PlayerExistsAsync(int id);

        Task EditPlayerAsync(int id, PlayerInputModel inputModel);

        Task<DeletePlayerViewModel> GetPlayerForDeleteByIdAsync(int id);

        Task DeletePlayerAsync(int id);

        Task<IEnumerable<PlayerViewModel>> SearchPlayerByFirstAndLastNameAsync(string? name);

        Task<bool> IsPlayerOwnedByUserAsync(int playerId, string userId);

        Task<IEnumerable<PlayerViewModel>> GetUsersPlayers(string userId);
    }
}
