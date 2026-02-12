using EuroLeaguePlayerBuilder.Data;
using EuroLeaguePlayerBuilder.Data.Models;
using EuroLeaguePlayerBuilder.Services.Core.Interfaces;
using EuroLeaguePlayerBuilder.ViewModels.Coaches;
using EuroLeaguePlayerBuilder.ViewModels.Players;
using EuroLeaguePlayerBuilder.ViewModels.Teams;
using Microsoft.EntityFrameworkCore;
using static EuroLeaguePlayerBuilder.GCommon.PlayerPositionHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EuroLeaguePlayerBuilder.ViewModels.Home;

namespace EuroLeaguePlayerBuilder.Services.Core
{
    public class TeamService : ITeamService
    {
        private readonly ApplicationDbContext _dbContext;

        public TeamService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<TeamViewModel>> GetAllTeamsAsync()
        {
            IEnumerable<TeamViewModel> allTeams = await _dbContext.Teams
                .Include(t => t.Players)
                .AsNoTracking()
                .Select(t => new TeamViewModel
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

        public async Task<TeamDetailsViewModel> GetTeamDetailsByIdAsync(int id)
        {
            Team? team = await _dbContext.Teams
                .Include(t => t.Coach)
                .Include(t => t.Players)
                .AsNoTracking()
                .SingleOrDefaultAsync(t => t.Id == id);

            if(team == null)
            {
                return null;
            }

            TeamDetailsViewModel teamDetails = new TeamDetailsViewModel
            {

                Name = team.Name,
                LogoPath = team.LogoPath,
                Coach = new CoachViewModel
                {
                    Id = team.Coach.Id,
                    FirstName = team.Coach.FirstName,
                    LastName = team.Coach.LastName,
                    TitlesWon = team.Coach.TitlesWon
                },
                Players = team.Players.Select(p => new PlayerViewModel
                {
                    Id = p.Id,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    Position = PositionToString[p.Position],
                })
                .OrderBy(pvm => pvm.FirstName)
                .ToList()
            };

            return teamDetails;
        }

        public async Task<IEnumerable<HomePageTeamViewModel>> GetTeamsForHomePageAsync()
        {
            IEnumerable<HomePageTeamViewModel> teams = await _dbContext
                .Teams
                .AsNoTracking()
                .Select(t => new HomePageTeamViewModel
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
