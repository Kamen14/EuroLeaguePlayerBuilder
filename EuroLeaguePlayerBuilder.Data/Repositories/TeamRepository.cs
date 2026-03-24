using EuroLeaguePlayerBuilder.Data.Models;
using EuroLeaguePlayerBuilder.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EuroLeaguePlayerBuilder.Data.Repositories
{
    public class TeamRepository : ITeamRepository, IDisposable
    {
        private readonly ApplicationDbContext _dbContext;
        private bool disposed = false;

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

        // Dispose pattern implementation
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
