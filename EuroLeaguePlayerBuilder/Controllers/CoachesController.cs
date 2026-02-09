using EuroLeaguePlayerBuilder.Data;
using EuroLeaguePlayerBuilder.ViewModels.Coaches;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EuroLeaguePlayerBuilder.Controllers
{
    public class CoachesController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public CoachesController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            IEnumerable<AllCoachesViewModel> coaches = _dbContext
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
                .ToList();

            return View(coaches);
        }
    }
}
