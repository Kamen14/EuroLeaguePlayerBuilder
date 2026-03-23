using EuroLeaguePlayerBuilder.Data;
using EuroLeaguePlayerBuilder.Data.Repositories.Interfaces;
using EuroLeaguePlayerBuilder.Services.Core.Interfaces;
using EuroLeaguePlayerBuilder.ViewModels.Coaches;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EuroLeaguePlayerBuilder.Services.Core
{
    public class CoachService : ICoachService
    {
        private readonly ITeamRepository _teamRepository;

        public CoachService(ITeamRepository teamRepository)
        {
            _teamRepository = teamRepository;
        }

        public async Task<IEnumerable<AllCoachesViewModel>> GetAllCoachesWithTeamsAsync()
        {
            IEnumerable<AllCoachesViewModel> coaches = await _teamRepository
                .GetAllTeamsNoTracking()
                .Select(t => new AllCoachesViewModel
                {
                    FirstName = t.Coach.FirstName,
                    LastName = t.Coach.LastName,
                    TitlesWon = t.Coach.TitlesWon,
                    TeamId = t.Id,
                    TeamLogoPath = t.LogoPath
                })
                .ToListAsync();

            return coaches;
        }
    }
}
