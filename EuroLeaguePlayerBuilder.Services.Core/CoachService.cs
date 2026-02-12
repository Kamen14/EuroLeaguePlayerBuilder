using EuroLeaguePlayerBuilder.Data;
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
        private readonly ApplicationDbContext _dbContext;

        public CoachService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<AllCoachesViewModel>> GetAllCoachesWithTeamsAsync()
        {
            IEnumerable<AllCoachesViewModel> coaches =  await _dbContext
                .Teams
                .Select(t => new AllCoachesViewModel
                {
                    FirstName = t.Coach.FirstName,
                    LastName = t.Coach.LastName,
                    TitlesWon = t.Coach.TitlesWon,
                    TeamId = t.Id,
                    TeamLogoPath = t.LogoPath
                })
                .AsNoTracking()
                .ToListAsync();

            return coaches;
        }
    }
}
