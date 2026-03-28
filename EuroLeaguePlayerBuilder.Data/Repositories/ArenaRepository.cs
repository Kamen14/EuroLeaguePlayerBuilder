using EuroLeaguePlayerBuilder.Data.Models;
using EuroLeaguePlayerBuilder.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace EuroLeaguePlayerBuilder.Data.Repositories
{
    public class ArenaRepository : IArenaRepository, IDisposable
    {
        private readonly ApplicationDbContext _dbContext;
        private bool disposed = false;

        public ArenaRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<Arena> GetAllArenasNoTracking()
        {
            return _dbContext.Arenas
                .AsNoTracking();
        }

        public async Task<bool> AddArenaAsync(Arena arena)
        {
            await _dbContext.Arenas.AddAsync(arena);
            int resultCount = await _dbContext.SaveChangesAsync();

            return resultCount == 1;
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
