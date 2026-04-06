using EuroLeaguePlayerBuilder.Data.Models;
using EuroLeaguePlayerBuilder.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EuroLeaguePlayerBuilder.Data.Repositories
{
    public class GameRepository : IGameRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private bool disposed = false;

        public GameRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public IQueryable<Game> GetAllGamesNoTracking()
        {
            return _dbContext.Games
                .AsNoTracking();
        }
        public IQueryable<Game> GetAllGames()
        {
            return _dbContext.Games;
        }

        public async Task<bool> AddGameAsync(Game game)
        {
            await _dbContext.Games.AddAsync(game);
            int resultCount = await _dbContext.SaveChangesAsync();

            return resultCount == 1;
        }

        public async Task UpdateGameAsync(Game selectedGame)
        {
            _dbContext.Games.Update(selectedGame);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteGameFromDbAsync(Game selectedGame)
        {
            _dbContext.Games.Remove(selectedGame);
            await _dbContext.SaveChangesAsync();
        }

        public IQueryable<Game> GetAllGamesWithDetailsNoTracking()
        {
            return _dbContext.Games
                .Include(g => g.TeamOne)
                .Include(g => g.TeamTwo)
                .Include(g => g.Arena)
                .Include(g => g.User)
                .AsNoTracking();
        }

        public IQueryable<Game> GetAllGamesWithUserNoTracking()
        {
            return _dbContext.Games
                .Include(a => a.User)
                .AsNoTracking();
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
