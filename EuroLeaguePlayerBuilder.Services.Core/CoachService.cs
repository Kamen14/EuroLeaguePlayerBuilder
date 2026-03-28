using EuroLeaguePlayerBuilder.Data.Repositories.Interfaces;
using EuroLeaguePlayerBuilder.Services.Core.Interfaces;
using EuroLeaguePlayerBuilder.Services.Models.Coaches;
using Microsoft.EntityFrameworkCore;

namespace EuroLeaguePlayerBuilder.Services.Core
{
    public class CoachService : ICoachService
    {
        private readonly ITeamRepository _teamRepository;

        public CoachService(ITeamRepository teamRepository)
        {
            _teamRepository = teamRepository;
        }

        public async Task<IEnumerable<AllCoachesDto>> GetAllCoachesWithTeamsAsync()
        {
            IEnumerable<AllCoachesDto> coaches = await _teamRepository
                .GetAllTeamsNoTracking()
                .Select(t => new AllCoachesDto
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
