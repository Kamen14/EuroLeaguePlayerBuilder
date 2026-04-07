using EuroLeaguePlayerBuilder.Data.Models;
using EuroLeaguePlayerBuilder.Services.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using static EuroLeaguePlayerBuilder.GCommon.PlayerPositionHelper;
using EuroLeaguePlayerBuilder.Data.Repositories.Interfaces;
using EuroLeaguePlayerBuilder.Services.Models.Teams;
using EuroLeaguePlayerBuilder.Services.Models.Coaches;
using EuroLeaguePlayerBuilder.Services.Models.Players;

namespace EuroLeaguePlayerBuilder.Services.Core
{
    public class TeamService : ITeamService
    {
        private readonly ITeamRepository _teamRepository;

        public TeamService(ITeamRepository teamRepository)
        {
            _teamRepository = teamRepository;
        }

        public async Task<IEnumerable<TeamDto>> GetAllTeamsAsync()
        {
            IEnumerable<TeamDto> allTeams = await _teamRepository
                .GetAllTeamsWithPlayersNoTracking()
                .Select(t => new TeamDto
                {
                    Id = t.Id,
                    Name = t.Name,
                    City = t.City,
                    Country = t.Country,
                    LogoPath = t.LogoPath,
                    PlayersCount = t.Players.Count
                })
                .OrderBy(tvm => tvm.Name)
                .ToListAsync();

            return allTeams;
        }

        public async Task<TeamDetailsDto> GetTeamDetailsByIdAsync(int id)
        {
            Team? team = await _teamRepository
                .GetTeamWithPlayersAndCoachByIdNoTrackingAsync(id);

            if(team == null)
            {
                return null;
            }

            TeamDetailsDto teamDetails = new TeamDetailsDto
            {

                Name = team.Name,
                LogoPath = team.LogoPath,
                Coach = new CoachDto
                {
                    Id = team.Coach.Id,
                    FirstName = team.Coach.FirstName,
                    LastName = team.Coach.LastName,
                    TitlesWon = team.Coach.TitlesWon
                },
                Players = team.Players.Select(p => new PlayerDto
                {
                    Id = p.Id,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    Position = PositionToString[p.Position],
                    UserId = p.UserId,
                })
                .OrderBy(pDto => pDto.FirstName)
                .ToList(),
                ArenaName = team.Arena!.Name,
                ArenaCapacity = team.Arena!.Capacity
            };

            return teamDetails;
        }

        public async Task<IEnumerable<HomePageTeamDto>> GetTeamsForHomePageAsync()
        {
            IEnumerable<HomePageTeamDto> teams = await _teamRepository
                .GetAllTeamsNoTracking()
                .Select(t => new HomePageTeamDto
                {
                    Id = t.Id,
                    Name = t.Name,
                    LogoPath = t.LogoPath
                })
                .OrderBy(t => t.Name)
                .ToListAsync();

            return teams;
        }
    }
}
