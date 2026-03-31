using EuroLeaguePlayerBuilder.Data.Models;
using EuroLeaguePlayerBuilder.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EuroLeaguePlayerBuilder.GCommon.EntityValidation;

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
