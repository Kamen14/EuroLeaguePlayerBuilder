using EuroLeaguePlayerBuilder.Data.Models;
using EuroLeaguePlayerBuilder.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace EuroLeaguePlayerBuilder.Data.Repositories
{
    public class PlayerRepository : IPlayerRepository, IDisposable
    {
        private readonly ApplicationDbContext _dbContext;
        private bool disposed = false;

        public PlayerRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<Player> GetAllPlayers()
        {
            return _dbContext.Players;
        }

        public IQueryable<Player> GetAllPlayersNoTracking()
        {
            return _dbContext.Players
                .AsNoTracking();
        }

        public IQueryable<Team> GetAllTeamsNoTracking()
        {
            return _dbContext.Teams
                .AsNoTracking();
        }

        public async Task<Player?> GetPlayerWithTeamByIdNoTrackingAsync(int id)
        {
            return await _dbContext.Players
                .Include(p => p.Team)
                .AsNoTracking()
                .SingleOrDefaultAsync(p => p.Id == id);
        }
        public async Task<bool> AddPlayerAsync(Player player)
        {
            await _dbContext.Players.AddAsync(player);
            int resultCount = await _dbContext.SaveChangesAsync();

            return resultCount == 1;
        }

        public async Task UpdatePlayerAsync(Player selectedPlayer)
        {
            _dbContext.Players.Update(selectedPlayer);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeletePlayerAsync(Player selectedPlayer)
        {
            _dbContext.Players.Remove(selectedPlayer);
            await _dbContext.SaveChangesAsync();
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
