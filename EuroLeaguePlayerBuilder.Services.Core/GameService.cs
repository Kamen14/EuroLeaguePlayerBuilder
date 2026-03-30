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
    }
}
