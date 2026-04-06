using EuroLeaguePlayerBuilder.Services.Core.Interfaces;
using static EuroLeaguePlayerBuilder.GCommon.PlayerPositionHelper;
using Microsoft.EntityFrameworkCore;
using EuroLeaguePlayerBuilder.Data.Models;
using EuroLeaguePlayerBuilder.Data.Repositories.Interfaces;
using EuroLeaguePlayerBuilder.Services.Models.Players;

namespace EuroLeaguePlayerBuilder.Services.Core
{
    public class PlayerService : IPlayerService
    {
        private readonly IPlayerRepository _playerRepository;

        public PlayerService(IPlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;
        }


        public async Task<IEnumerable<PlayerDto>> GetAllPlayersOrderedByNameAsync()
        {
            IEnumerable<PlayerDto> allPlayers = await _playerRepository
                .GetAllPlayersNoTracking()
                .Select(p => new PlayerDto
                {
                    Id = p.Id,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    Position = PositionToString[p.Position],
                    UserId = p.UserId
                })
                .OrderBy(pvm => pvm.FirstName)
                .ThenBy(pvm => pvm.LastName)
                .ToListAsync();

            return allPlayers;
        }

        public async Task<PlayerDetailsDto> GetPlayerDetailsByIdAsync(int id)
        {
            Player? player = await _playerRepository
                .GetPlayerWithTeamByIdNoTrackingAsync(id);

            if (player == null)
            {
                return null;
            }

            PlayerDetailsDto detailDto = new PlayerDetailsDto
            {
                FirstName = player.FirstName,
                LastName = player.LastName,
                Position = PositionToString[player.Position],
                PointsPerGame = player.PointsPerGame,
                ReboundsPerGame = player.ReboundsPerGame,
                AssistsPerGame = player.AssistsPerGame,
                TeamId = player.TeamId,
                TeamName = player.Team.Name
            };

            return detailDto;
        }

        public async Task<PlayerInputDto> GetPlayerInputModelWithLoadedTeamsAsync()
        {
            PlayerInputDto inputModel = new PlayerInputDto
            {
                Teams = await LoadTeamsDropdownAsync()
            };

            return inputModel;
        }

        public async Task<IEnumerable<CreatePlayerTeamDto>> LoadTeamsDropdownAsync()
        {
            IEnumerable<CreatePlayerTeamDto> loadedTeams = await _playerRepository
                .GetAllTeamsNoTracking()
                     .Select(t => new CreatePlayerTeamDto
                     {
                         Id = t.Id,
                         Name = t.Name
                     })
                     .OrderBy(t => t.Name)
                     .ToListAsync();

            return loadedTeams;
        }

        public async Task CreatePlayerAsync(PlayerInputDto inputDto, string userId)
        {
            Player player = new Player
            {
                FirstName = inputDto.FirstName,
                LastName = inputDto.LastName,
                Position = inputDto.Position,
                PointsPerGame = inputDto.PointsPerGame,
                ReboundsPerGame = inputDto.ReboundsPerGame,
                AssistsPerGame = inputDto.AssistsPerGame,
                TeamId = inputDto.TeamId,
                UserId = userId
            };

            bool successfullyAdded = await _playerRepository.AddPlayerAsync(player);

            if(!successfullyAdded)
            {
                throw new DbUpdateException("An error occurred while adding the player to the database.");
            }
        }

        public async Task<PlayerInputDto> GetPlayerInputModelWithLoadedTeamsAndPlayerDataAsync(int id)
        {
            Player? player = await _playerRepository
                .GetPlayerWithTeamByIdNoTrackingAsync(id);

            if (player == null)
            {
                return null;
            }

            PlayerInputDto inputDto = new PlayerInputDto
            {
                FirstName = player.FirstName,
                LastName = player.LastName,
                Position = player.Position,
                PointsPerGame = player.PointsPerGame,
                ReboundsPerGame = player.ReboundsPerGame,
                AssistsPerGame = player.AssistsPerGame,
                TeamId = player.TeamId,
                Teams = await LoadTeamsDropdownAsync(),
            };

            return inputDto;
        }

        public async Task<bool> PlayerExistsAsync(int id)
        {
            bool selectedPlayerExists = await _playerRepository
                .GetAllPlayersNoTracking()
                .AnyAsync(p => p.Id == id);

            return selectedPlayerExists;
        }

        public async Task EditPlayerAsync(int id, PlayerInputDto inputDto)
        {
            Player? selectedPlayer = await _playerRepository
                .GetAllPlayers()
                .SingleOrDefaultAsync(p => p.Id == id);

            if(selectedPlayer == null)
            {
                throw new ArgumentException("Player with the provided ID does not exist.");
            }

            selectedPlayer.FirstName = inputDto.FirstName;
            selectedPlayer.LastName = inputDto.LastName;
            selectedPlayer.Position = inputDto.Position;
            selectedPlayer.PointsPerGame = inputDto.PointsPerGame;
            selectedPlayer.ReboundsPerGame = inputDto.ReboundsPerGame;
            selectedPlayer.AssistsPerGame = inputDto.AssistsPerGame;
            selectedPlayer.TeamId = inputDto.TeamId;

            await _playerRepository.UpdatePlayerAsync(selectedPlayer);
        }

        public async Task<DeletePlayerDto> GetPlayerForDeleteByIdAsync(int id)
        {
            Player? player = await _playerRepository
               .GetAllPlayers()
               .SingleOrDefaultAsync(p => p.Id == id);

            if (player == null)
            {
                return null;
            }

            DeletePlayerDto deleteDto = new DeletePlayerDto
            {
                FirstName = player.FirstName,
                LastName = player.LastName
            };

            return deleteDto;
        }

        public async Task DeletePlayerAsync(int id)
        {
            Player? selectedPlayer = await _playerRepository
                .GetAllPlayers()
                .SingleOrDefaultAsync(p => p.Id == id);

            if (selectedPlayer == null)
            {
                throw new ArgumentException("Player with the provided ID does not exist.");
            }

            await _playerRepository.DeletePlayerFromDbAsync(selectedPlayer);
        }

        public async Task<IEnumerable<PlayerDto>> SearchPlayerByFirstAndLastNameAsync(string? name)
        {
            IQueryable<PlayerDto> playersQuery = _playerRepository
                .GetAllPlayersNoTracking()
               .Select(p => new PlayerDto
               {
                   Id = p.Id,
                   FirstName = p.FirstName,
                   LastName = p.LastName,
                   Position = PositionToString[p.Position],
                   UserId = p.UserId
               });


            if (!string.IsNullOrWhiteSpace(name))
            {
                playersQuery = playersQuery
                    .Where(p => p.FirstName.ToLower().Contains(name.ToLower())
                    || p.LastName.ToLower().Contains(name.ToLower()));
            }

            IEnumerable<PlayerDto> filteredPlayers = await playersQuery
                .OrderBy(pvm => pvm.FirstName)
                .ThenBy(pvm => pvm.LastName)
                .ToListAsync();

            return filteredPlayers;
        }

        public  async Task<bool> IsPlayerOwnedByUserAsync(int playerId, string userId)
        {
            string? playerUserId = await _playerRepository
                .GetAllPlayersNoTracking()
                .Where(p => p.Id == playerId)
                .Select(p => p.UserId)
                .SingleOrDefaultAsync();

            return playerUserId != null && playerUserId == userId;
        }

        public async Task<IEnumerable<PlayerDto>> GetUserPlayers(string userId)
        {
            return await _playerRepository
                .GetAllPlayersNoTracking()
                .Where(p => p.UserId == userId)
                .Select(p => new PlayerDto
                {
                    Id = p.Id,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    Position = PositionToString[p.Position],
                    UserId = p.UserId
                })
                .OrderBy(pvm => pvm.FirstName)
                .ThenBy(pvm => pvm.LastName)
                .ToListAsync();
        }

        public async Task<IEnumerable<AdminPlayerDto>> GetAllPlayersForAdminAsync()
        {
            IEnumerable<AdminPlayerDto> allPlayers = await _playerRepository
                .GetAllPlayersNoTracking()
                .Select(p => new AdminPlayerDto
                {
                    Id = p.Id,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    Position = PositionToString[p.Position],
                    CreatedByEmail = p.User != null ? p.User.Email : null,
                    CreatedByNickname = p.User != null ? p.User.Nickname : null
                })
                .OrderBy(p => p.CreatedByEmail == null ? 1 : 0)
                .ThenBy(p => p.CreatedByNickname ?? p.CreatedByEmail)
                .ThenBy(p => p.FirstName)
                .ThenBy(p => p.LastName)
                .ToListAsync();

            return allPlayers;
        }

        public async Task<bool> IsPlayerUserCreatedAsync(int playerId)
        {
            string? playerUserId = await _playerRepository
                .GetAllPlayersNoTracking()
                .Where(p => p.Id == playerId)
                .Select(p => p.UserId)
                .SingleOrDefaultAsync();

            return playerUserId != null;
        }
    }
}
