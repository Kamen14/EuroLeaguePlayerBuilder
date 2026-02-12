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

namespace EuroLeaguePlayerBuilder.Services.Core
{
    public class PlayerService : IPlayerService
    {
        private readonly ApplicationDbContext _dbContext;

        public PlayerService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<IEnumerable<PlayerViewModel>> GetAllPlayersOrderedByNameAsync()
        {
            IEnumerable<PlayerViewModel> allPlayers = await _dbContext.Players
                .AsNoTracking()
                .Select(p => new PlayerViewModel
                {
                    Id = p.Id,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    Position = PositionToString[p.Position],
                })
                .OrderBy(pvm => pvm.FirstName)
                .ThenBy(pvm => pvm.LastName)
                .ToListAsync();

            return allPlayers;
        }

        public async Task<PlayerDetailsViewModel> GetPlayerDetailsByIdAsync(int id)
        {
            Player? player = await _dbContext.Players
                .Include(p => p.Team)
                .AsNoTracking()
                .SingleOrDefaultAsync(p => p.Id == id);

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
            IEnumerable<CreatePlayerTeamViewModel> loadedTeams = await _dbContext.Teams
                .AsNoTracking()
                     .Select(t => new CreatePlayerTeamViewModel
                     {
                         Id = t.Id,
                         Name = t.Name
                     })
                     .OrderBy(t => t.Name)
                     .ToListAsync();

            return loadedTeams;
        }

        public async Task CreatePlayerAsync(PlayerInputModel inputModel)
        {
            Player player = new Player
            {
                FirstName = inputModel.FirstName,
                LastName = inputModel.LastName,
                Position = inputModel.Position,
                PointsPerGame = inputModel.PointsPerGame,
                ReboundsPerGame = inputModel.ReboundsPerGame,
                AssistsPerGame = inputModel.AssistsPerGame,
                TeamId = inputModel.TeamId
            };

            await _dbContext.Players.AddAsync(player);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<PlayerInputModel> GetPlayerInputModelWithLoadedTeamsAndPlayerDataAsync(int id)
        {
            Player? player = await _dbContext.Players
                .Include(p => p.Team)
                .AsNoTracking()
                .SingleOrDefaultAsync(p => p.Id == id);

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
                Teams = await LoadTeamsDropdownAsync()
            };

            return inputModel;
        }

        public async Task<bool> PlayerExistsAsync(int id)
        {
            bool selectedPlayerExists = await _dbContext.Players
                .AnyAsync(p => p.Id == id);

            return selectedPlayerExists;
        }

        public async Task EditPlayerAsync(int id, PlayerInputModel inputModel)
        {
            Player? selectedPlayer = await _dbContext.Players
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

            _dbContext.Players.Update(selectedPlayer);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<DeletePlayerViewModel> GetPlayerForDeleteByIdAsync(int id)
        {
            Player? player = await _dbContext.Players
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
            Player? selectedPlayer = await _dbContext.Players
                .SingleOrDefaultAsync(p => p.Id == id);

            if (selectedPlayer == null)
            {
                throw new ArgumentException("Player with the provided ID does not exist.");
            }

            _dbContext.Players.Remove(selectedPlayer);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<PlayerViewModel>> SearchPlayerByFirstAndLastNameAsync(string? name)
        {
            IQueryable<PlayerViewModel> playersQuery = _dbContext.Players
               .Select(p => new PlayerViewModel
               {
                   Id = p.Id,
                   FirstName = p.FirstName,
                   LastName = p.LastName,
                   Position = PositionToString[p.Position],
               })
               .AsNoTracking();


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
    }
}
