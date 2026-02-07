using EuroLeaguePlayerBuilder.Data;
using EuroLeaguePlayerBuilder.Models;
using EuroLeaguePlayerBuilder.ViewModels.Players;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static EuroLeaguePlayerBuilder.Common.PlayerPositionHelper;

namespace EuroLeaguePlayerBuilder.Controllers
{
    public class PlayersController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public PlayersController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Index()
        {
            IEnumerable<PlayerViewModel> allPlayers = _dbContext.Players
                .Select(p => new PlayerViewModel
                {
                    Id = p.Id,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    Position = PositionToString[p.Position],
                })
                .OrderBy(pvm => pvm.FirstName)
                .ThenBy(pvm => pvm.LastName)
                .AsNoTracking()
                .ToList();

            return View(allPlayers);
        }

        public IActionResult Details(int id)
        {
            if(id <= 0)
            {
                return BadRequest();
            }

            Player? player = _dbContext.Players
                .Include(p => p.Team)
                .AsNoTracking()
                .SingleOrDefault(p => p.Id == id);

            if(player == null)
            {
                return NotFound();
            }

            PlayerDetailsViewModel viewModel = new PlayerDetailsViewModel
            {
                Id = player.Id,
                FirstName = player.FirstName,
                LastName = player.LastName,
                Position = PositionToString[player.Position],
                PointsPerGame = player.PointsPerGame,
                ReboundsPerGame = player.ReboundsPerGame,
                AssistsPerGame = player.AssistsPerGame,
                TeamId = player.TeamId,
                TeamName = player.Team.Name
            };

            return View(viewModel);
        }

        /*
        [HttpGet]
        public IActionResult Create()
        {

        }

        [HttpPost]
        public IActionResult Create()
        {
        }
        */
    }
}
