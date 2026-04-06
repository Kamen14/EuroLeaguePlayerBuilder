using EuroLeaguePlayerBuilder.Data.Models;
using EuroLeaguePlayerBuilder.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EuroLeaguePlayerBuilder.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public UserRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<ApplicationUser>> GetAllUsersAsync()
        {
            IQueryable<ApplicationUser> users = _dbContext.Users;

            IEnumerable<ApplicationUser> result = await users
                .OrderBy(u => u.Email)
                .ToListAsync();

            return result;
        }
    }
}
