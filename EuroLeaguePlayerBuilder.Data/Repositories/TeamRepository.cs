using EuroLeaguePlayerBuilder.Data.Models;
using EuroLeaguePlayerBuilder.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EuroLeaguePlayerBuilder.Data.Repositories
{
    public class TeamRepository : ITeamRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public TeamRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<Team> GetAllTeamsNoTracking()
        {
            return _dbContext.Teams
                .AsNoTracking();
        }

        public IQueryable<Team> GetAllTeamsWithPlayersNoTracking()
        {
            return _dbContext.Teams
                .Include(t => t.Players)
                .AsNoTracking();
        }

        public async Task<Team?> GetTeamWithPlayersAndCoachByIdNoTrackingAsync(int id)
        {
            return await _dbContext.Teams
                .Include(t => t.Coach)
                .Include(t => t.Players)
                .AsNoTracking()
                .SingleOrDefaultAsync(t => t.Id == id);
        }
    }
}
