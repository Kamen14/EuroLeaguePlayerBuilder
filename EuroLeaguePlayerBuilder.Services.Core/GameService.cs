using EuroLeaguePlayerBuilder.Data.Models;
using EuroLeaguePlayerBuilder.Data.Repositories;
using EuroLeaguePlayerBuilder.Data.Repositories.Interfaces;
using EuroLeaguePlayerBuilder.Services.Core.Interfaces;
using EuroLeaguePlayerBuilder.Services.Models.Arenas;
using EuroLeaguePlayerBuilder.Services.Models.Games;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EuroLeaguePlayerBuilder.GCommon.EntityValidation;

namespace EuroLeaguePlayerBuilder.Services.Core
{
    public class GameService : IGameService
    {
        private readonly IGameRepository _gameRepository;
        private readonly ITeamRepository _teamRepository;
        private readonly IArenaRepository _arenaRepository;

        public GameService(IGameRepository gameRepository, 
            ITeamRepository teamRepository, IArenaRepository arenaRepository )
        {
            _gameRepository = gameRepository;
            _teamRepository = teamRepository;
            _arenaRepository = arenaRepository;
        }

        public async Task<IEnumerable<GameDto>> GetAllGamesOrderedByTeamsNameAsync()
        {
            IEnumerable<GameDto> games = await _gameRepository.GetAllGamesNoTracking()
                .Select(g => new GameDto
                {
                    Id = g.Id,
                    TeamOneName = g.TeamOne.Name,
                    TeamOneLogoPath = g.TeamOne.LogoPath,
                    TeamOneScore = g.TeamOneScore,
                    TeamTwoName = g.TeamTwo.Name,
                    TeamTwoLogoPath = g.TeamTwo.LogoPath,
                    TeamTwoScore = g.TeamTwoScore,
                    ArenaName = g.Arena.Name

                })
                .OrderBy(g => g.TeamOneName)
                .ThenBy(g => g.TeamTwoName)
                .ToListAsync();

            return games;
        }

        public async Task<GameInputDto> GetGameInputDataAsync()
        {
            IEnumerable<GameTeamDto> gameTeamDtos = await _teamRepository
                .GetAllTeamsNoTracking()
                .Select(t => new GameTeamDto
                {
                    Id = t.Id,
                    Name = t.Name,
                    LogoPath = t.LogoPath,
                })
                .OrderBy(tDto => tDto.Name)
                .ToListAsync();

            IEnumerable<GameArenaDto> gameArenaDtos = await _arenaRepository
                .GetAllArenasNoTracking()
                .Select(a => new GameArenaDto
                {
                    Id = a.Id,
                    Name = a.Name,
                })
                .OrderBy(aDto => aDto.Name)
                .ToListAsync();

            GameInputDto gameInputDto = new GameInputDto
            {
                Teams = gameTeamDtos,
                Arenas = gameArenaDtos,
            };
            
            return gameInputDto;
        }

        public async Task CreateGameAsync(GameInputDto inputDto, string userId)
        {
            Game game = new Game
            {
                TeamOneId = inputDto.TeamOneId,
                TeamOneScore = 0,
                TeamTwoId = inputDto.TeamTwoId,
                TeamTwoScore = 0,
                ArenaId = inputDto.ArenaId,
                UserId = userId
            };

            bool successfullyAdded = await _gameRepository.AddGameAsync(game);

            if (!successfullyAdded)
            {
                throw new DbUpdateException("An error occurred while adding the game to the database.");
            }
        }

        public async Task<bool> GameExistsAsync(int id)
        {
            bool selectedGameExists = await _gameRepository
                .GetAllGamesNoTracking()
                .AnyAsync(g => g.Id == id);

            return selectedGameExists;
        }

        public async Task<bool> IsGameOwnedByUserAsync(int gameId, string userId)
        {
            string? gameUserId = await _gameRepository
                .GetAllGamesNoTracking()
                .Where(g => g.Id == gameId)
                .Select(g => g.UserId)
                .SingleOrDefaultAsync();

            return gameUserId != null && gameUserId == userId;
        }

        public async Task UpdateGameScoreAsync(int gameId, int teamOneScore, int teamTwoScore)
        {
            Game? selectedGame = _gameRepository
                .GetAllGames()
                .SingleOrDefault(g => g.Id == gameId);

            if(selectedGame == null)
            {
                throw new ArgumentException("The specified game does not exist.");
            }

            selectedGame.TeamOneScore = teamOneScore;
            selectedGame.TeamTwoScore = teamTwoScore;

            await _gameRepository.UpdateGameAsync(selectedGame);
        }

        public async Task<IEnumerable<GameDto>> GetUserGamesAsync(string userId)
        {
            return await _gameRepository
                .GetAllGamesNoTracking()
                .Where(g => g.UserId == userId)
                .Select(g => new GameDto
                {
                    Id = g.Id,
                    TeamOneName = g.TeamOne.Name,
                    TeamOneLogoPath = g.TeamOne.LogoPath,
                    TeamTwoName = g.TeamTwo.Name,
                    TeamTwoLogoPath = g.TeamTwo.LogoPath,
                    TeamOneScore = g.TeamOneScore,
                    TeamTwoScore = g.TeamTwoScore,
                    ArenaName = g.Arena.Name
                })
                .OrderBy(g => g.TeamOneName)
                .ThenBy(g => g.TeamTwoName)
                .ToListAsync();
        }

        public async Task<GameDeleteDto> GetGameForDeleteByIdAsync(int id)
        {
            Game? game = await _gameRepository
               .GetAllGames()
               .Include(g => g.TeamOne)
               .Include(g => g.TeamTwo)
               .SingleOrDefaultAsync(g => g.Id == id);

            if (game == null)
            {
                return null;
            }

            GameDeleteDto deleteDto = new GameDeleteDto
            {
                TeamOneName = game.TeamOne.Name,
                TeamTwoName = game.TeamTwo.Name
            };

            return deleteDto;
        }

        public async Task DeleteGameAsync(int id)
        {
            Game? selectedGame = await _gameRepository
               .GetAllGames()
               .SingleOrDefaultAsync(g => g.Id == id);

            if (selectedGame == null)
            {
                throw new ArgumentException("Player with the provided ID does not exist.");
            }

            await _gameRepository.DeleteGameFromDbAsync(selectedGame);
        }

        public async Task<IEnumerable<AdminGameDto>> GetAllArenasForAdminAsync()
        {
            IEnumerable<AdminGameDto> allGames = await _gameRepository
                .GetAllGames()
                .OrderBy(g => g.TeamOne.Name)
                .ThenBy(g => g.TeamTwo.Name)
                .Select(g => new AdminGameDto
                {
                    Id = g.Id,
                    TeamOneLogoPath = g.TeamOne.LogoPath,
                    TeamOneScore = g.TeamOneScore,
                    TeamTwoLogoPath = g.TeamTwo.LogoPath,
                    TeamTwoScore = g.TeamTwoScore,
                    ArenaName = g.Arena.Name,
                    CreatedByEmail = g.User != null ? g.User.Email : null,
                    CreatedByNickname = g.User != null ? g.User.Nickname : null
                })
                .ToListAsync();

            return allGames;
        }
    }
}
