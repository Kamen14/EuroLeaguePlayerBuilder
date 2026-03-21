using EuroLeaguePlayerBuilder.Data;
using EuroLeaguePlayerBuilder.Services.Core.Interfaces;
using EuroLeaguePlayerBuilder.ViewModels.Players;
using static EuroLeaguePlayerBuilder.GCommon.PlayerPositionHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EuroLeaguePlayerBuilder.Data.Models;
using EuroLeaguePlayerBuilder.Data.Repositories;
using EuroLeaguePlayerBuilder.Data.Repositories.Interfaces;

namespace EuroLeaguePlayerBuilder.Services.Core
{
    public class PlayerService : IPlayerService
    {
        private readonly IPlayerRepository _playerRepository;

        public PlayerService(IPlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;
        }


        public async Task<IEnumerable<PlayerViewModel>> GetAllPlayersOrderedByNameAsync()
        {
            IEnumerable<PlayerViewModel> allPlayers = await _playerRepository
                .GetAllPlayersNoTracking()
                .Select(p => new PlayerViewModel
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

        public async Task<PlayerDetailsViewModel> GetPlayerDetailsByIdAsync(int id)
        {
            Player? player = await _playerRepository
                .GetPlayerWithTeamByIdNoTrackingAsync(id);

            if (player == null)
            {
                return null;
            }

            PlayerDetailsViewModel viewModel = new PlayerDetailsViewModel
            {
                Id = player.Id,
                FirstName = player.FirstName,
                LastName = player.LastName,
                Position = PositionToString[player.Position],
                PointsPerGame = player.PointsPerGame,
                ReboundsPerGame = player.ReboundsPerGame,
                AssistsPerGame = player.AssistsPerGame,
                TeamId = player.TeamId,
                TeamName = player.Team.Name
            };

            return viewModel;
        }

        public async Task<PlayerInputModel> GetPlayerInputModelWithLoadedTeamsAsync()
        {
            PlayerInputModel inputModel = new PlayerInputModel
            {
                Teams = await LoadTeamsDropdownAsync()
            };

            return inputModel;
        }

        public async Task<IEnumerable<CreatePlayerTeamViewModel>> LoadTeamsDropdownAsync()
        {
            IEnumerable<CreatePlayerTeamViewModel> loadedTeams = await _playerRepository
                .GetAllTeamsNoTracking()
                     .Select(t => new CreatePlayerTeamViewModel
                     {
                         Id = t.Id,
                         Name = t.Name
                     })
                     .OrderBy(t => t.Name)
                     .ToListAsync();

            return loadedTeams;
        }

        public async Task CreatePlayerAsync(PlayerInputModel inputModel, string userId)
        {
            Player player = new Player
            {
                FirstName = inputModel.FirstName,
                LastName = inputModel.LastName,
                Position = inputModel.Position,
                PointsPerGame = inputModel.PointsPerGame,
                ReboundsPerGame = inputModel.ReboundsPerGame,
                AssistsPerGame = inputModel.AssistsPerGame,
                TeamId = inputModel.TeamId,
                UserId = userId
            };

                bool successfullyAdded = await _playerRepository.AddPlayerAsync(player);

                if(!successfullyAdded)
                {
                    throw new DbUpdateException("An error occurred while adding the player to the database.");
                }
        }

        public async Task<PlayerInputModel> GetPlayerInputModelWithLoadedTeamsAndPlayerDataAsync(int id)
        {
            Player? player = await _playerRepository
                .GetPlayerWithTeamByIdNoTrackingAsync(id);

            if (player == null)
            {
                return null;
            }

            PlayerInputModel inputModel = new PlayerInputModel
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

            return inputModel;
        }

        public async Task<bool> PlayerExistsAsync(int id)
        {
            bool selectedPlayerExists = await _playerRepository
                .GetAllPlayersNoTracking()
                .AnyAsync(p => p.Id == id);

            return selectedPlayerExists;
        }

        public async Task EditPlayerAsync(int id, PlayerInputModel inputModel)
        {
            Player? selectedPlayer = await _playerRepository
                .GetAllPlayers()
                .SingleOrDefaultAsync(p => p.Id == id);

            if(selectedPlayer == null)
            {
                throw new ArgumentException("Player with the provided ID does not exist.");
            }

            selectedPlayer.FirstName = inputModel.FirstName;
            selectedPlayer.LastName = inputModel.LastName;
            selectedPlayer.Position = inputModel.Position;
            selectedPlayer.PointsPerGame = inputModel.PointsPerGame;
            selectedPlayer.ReboundsPerGame = inputModel.ReboundsPerGame;
            selectedPlayer.AssistsPerGame = inputModel.AssistsPerGame;
            selectedPlayer.TeamId = inputModel.TeamId;

            await _playerRepository.UpdatePlayerAsync(selectedPlayer);
        }

        public async Task<DeletePlayerViewModel> GetPlayerForDeleteByIdAsync(int id)
        {
            Player? player = await _playerRepository
               .GetAllPlayers()
               .SingleOrDefaultAsync(p => p.Id == id);

            if (player == null)
            {
                return null;
            }

            DeletePlayerViewModel deleteViewModel = new DeletePlayerViewModel
            {
                FirstName = player.FirstName,
                LastName = player.LastName
            };

            return deleteViewModel;
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

            await _playerRepository.DeletePlayerAsync(selectedPlayer);
        }

        public async Task<IEnumerable<PlayerViewModel>> SearchPlayerByFirstAndLastNameAsync(string? name)
        {
            IQueryable<PlayerViewModel> playersQuery = _playerRepository
                .GetAllPlayersNoTracking()
               .Select(p => new PlayerViewModel
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

            IEnumerable<PlayerViewModel> filteredPlayers = await playersQuery
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

        public async Task<IEnumerable<PlayerViewModel>> GetUsersPlayers(string userId)
        {
            return await _playerRepository
                .GetAllPlayersNoTracking()
                .Where(p => p.UserId == userId)
                .Select(p => new PlayerViewModel
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
    }
}
