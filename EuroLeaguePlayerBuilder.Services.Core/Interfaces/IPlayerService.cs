using EuroLeaguePlayerBuilder.Services.Models.Players;

namespace EuroLeaguePlayerBuilder.Services.Core.Interfaces
{
    public interface IPlayerService 
    {
        Task<IEnumerable<PlayerDto>> GetAllPlayersOrderedByNameAsync();

        Task<PlayerDetailsDto> GetPlayerDetailsByIdAsync(int id);

        Task<PlayerInputDto> GetPlayerInputModelWithLoadedTeamsAsync();

        Task<IEnumerable<CreatePlayerTeamDto>> LoadTeamsDropdownAsync();

        Task CreatePlayerAsync(PlayerInputDto inputDto, string userId);

        Task <PlayerInputDto> GetPlayerInputModelWithLoadedTeamsAndPlayerDataAsync(int id);

        Task<bool> PlayerExistsAsync(int id);

        Task EditPlayerAsync(int id, PlayerInputDto inputDto);

        Task<DeletePlayerDto> GetPlayerForDeleteByIdAsync(int id);

        Task DeletePlayerAsync(int id);

        Task<IEnumerable<PlayerDto>> SearchPlayerByFirstAndLastNameAsync(string? name);

        Task<bool> IsPlayerOwnedByUserAsync(int playerId, string userId);

        Task<IEnumerable<PlayerDto>> GetUserPlayers(string userId);

        Task<IEnumerable<AdminPlayerDto>> GetAllPlayersForAdminAsync();

        Task<bool> IsPlayerUserCreatedAsync(int playerId);
    }
}
