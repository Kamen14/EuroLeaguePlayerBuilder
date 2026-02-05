using EuroLeaguePlayerBuilder.Data;
using EuroLeaguePlayerBuilder.Models;
using EuroLeaguePlayerBuilder.ViewModels.Teams;
using EuroLeaguePlayerBuilder.ViewModels.Players;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EuroLeaguePlayerBuilder.ViewModels.Coaches;

namespace EuroLeaguePlayerBuilder.Controllers
{
    public class TeamsController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        public TeamsController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Index()
        {
            IEnumerable<TeamViewModel> allTeams = _dbContext.Teams
                .Include(t => t.Players)
                .Select(t => new TeamViewModel
                {
                    Id = t.Id,
                    Name = t.Name,
                    City = t.City,
                    Country = t.Country,
                    LogoPath = t.LogoPath,
                    PlayersCount = t.Players.Count
                })
                .AsNoTracking()
                .ToList();

            return View(allTeams);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            if(id <= 0)
            {
                return BadRequest();
            }

            Team? team = _dbContext.Teams
                .Include(t => t.Coach)
                .Include(t => t.Players)
                .AsNoTracking()
                .SingleOrDefault(t => t.Id == id);

            if(team == null)
            {
                return NotFound();
            }

            TeamDetailsViewModel teamDetails = new TeamDetailsViewModel
            {

                Name = team.Name,
                LogoPath = team.LogoPath,
                Coach = new CoachViewModel
                {
                    Id = team.Coach.Id,
                    FirstName = team.Coach.FirstName,
                    LastName = team.Coach.LastName,
                },
                Players = team.Players.Select(p => new PlayerViewModel
                {
                    Id = p.Id,
                    FirstName = p.FirstName,
                    Position = p.Position.ToString(),
                }).ToList()
            };

            return View(teamDetails);
        }
    }
}
