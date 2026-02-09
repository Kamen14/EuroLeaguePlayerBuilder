using EuroLeaguePlayerBuilder.Data;
using EuroLeaguePlayerBuilder.Data.Enums;
using EuroLeaguePlayerBuilder.Models;
using EuroLeaguePlayerBuilder.ViewModels.Players;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
            if (id <= 0)
            {
                return BadRequest();
            }

            Player? player = _dbContext.Players
                .Include(p => p.Team)
                .AsNoTracking()
                .SingleOrDefault(p => p.Id == id);

            if (player == null)
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


        [HttpGet]
        public IActionResult Create()
        {
            PlayerInputModel inputModel = new PlayerInputModel
            {
                Teams = LoadTeamsDropdown()
            };

            return View(inputModel);
        }


        [HttpPost]
        public IActionResult Create(PlayerInputModel inputModel)
        {
            inputModel.Teams = LoadTeamsDropdown();

            if (!ModelState.IsValid)
            {
                return View(inputModel);
            }
            
            if(!TeamExists(inputModel.TeamId))
            {
                ModelState.AddModelError(nameof(inputModel.TeamId), "Selected team does not exist.");
                
                return View(inputModel);
            }

            if (!Enum.IsDefined(typeof(Position), inputModel.Position))
            {
                ModelState.AddModelError(nameof(inputModel.Position), "Selected position is invalid.");

                return View(inputModel);
            }

            try
            {
                Player player = new Player
                {
                    FirstName = inputModel.FirstName,
                    LastName = inputModel.LastName,
                    Position = inputModel.Position,
                    PointsPerGame = inputModel.PointsPerGame,
                    ReboundsPerGame = inputModel.ReboundsPerGame,
                    AssistsPerGame = inputModel.AssistsPerGame,
                    TeamId = inputModel.TeamId
                };

                _dbContext.Players.Add(player);
                _dbContext.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while creating the player. Please try again.");

                return View(inputModel);
            }
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            Player? player = _dbContext.Players
                .Include(p => p.Team)
                .AsNoTracking()
                .SingleOrDefault(p => p.Id == id);

            if (player == null)
            {
                return NotFound();
            }

            PlayerInputModel inputModel = new PlayerInputModel
            {
                FirstName = player.FirstName,
                LastName = player.LastName,
                Position = player.Position,
                PointsPerGame = player.PointsPerGame,
                ReboundsPerGame = player.ReboundsPerGame,
                AssistsPerGame = player.AssistsPerGame,
                TeamId = player.TeamId,
                Teams = LoadTeamsDropdown()
            };
            return View(inputModel);
        }

        [HttpPost]
        public IActionResult Edit([FromRoute] int id, PlayerInputModel inputModel)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            Player? selectedPlayer = _dbContext.Players
                .Include(p => p.Team)
                .SingleOrDefault(p => p.Id == id);

            if (selectedPlayer == null)
            {
                return NotFound();
            }

            inputModel.Teams = LoadTeamsDropdown();

            if (!ModelState.IsValid)
            {
                return View(inputModel);
            }

            if (!TeamExists(inputModel.TeamId))
            {
                ModelState.AddModelError(nameof(inputModel.TeamId), "Selected team does not exist.");

                return View(inputModel);
            }

            if (!Enum.IsDefined(typeof(Position), inputModel.Position))
            {
                ModelState.AddModelError(nameof(inputModel.Position), "Selected position is invalid.");

                return View(inputModel);
            }

            try
            {
                selectedPlayer.FirstName = inputModel.FirstName;
                selectedPlayer.LastName = inputModel.LastName;
                selectedPlayer.Position = inputModel.Position;
                selectedPlayer.PointsPerGame = inputModel.PointsPerGame;
                selectedPlayer.ReboundsPerGame = inputModel.ReboundsPerGame;
                selectedPlayer.AssistsPerGame = inputModel.AssistsPerGame;
                selectedPlayer.TeamId = inputModel.TeamId;

                _dbContext.SaveChanges();
                
                return RedirectToAction(nameof(Details), new { id });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "An error occured while editing the player. Please try again.");

                return View(inputModel);
            }
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            Player? player = _dbContext.Players
                .Include(p => p.Team)
                .AsNoTracking()
                .SingleOrDefault(p => p.Id == id);

            if (player == null)
            {
                return NotFound();
            }

            DeletePlayerViewModel deleteViewModel = new DeletePlayerViewModel
            {
                FirstName = player.FirstName,
                LastName = player.LastName
            };

            return View(deleteViewModel);
        }

        [HttpPost]
        public IActionResult Delete([FromRoute]int id, DeletePlayerViewModel? viewModel)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            Player? selectedPlayer = _dbContext.Players
                .Include(p => p.Team)
                .SingleOrDefault(p => p.Id == id);

            if (selectedPlayer == null)
            {
                return NotFound();
            }

            try
            {
                _dbContext.Players.Remove(selectedPlayer);
                _dbContext.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "An error occurred while deleting the player. Please try again.");
                return View(viewModel);
            }
        }

        
        public IActionResult Search(string? name)
        {
            IQueryable<PlayerViewModel> playersQuery = _dbContext.Players
               .Select(p => new PlayerViewModel
               {
                   Id = p.Id,
                   FirstName = p.FirstName,
                   LastName = p.LastName,
                   Position = PositionToString[p.Position],
               })
               .AsNoTracking();


            if (!string.IsNullOrWhiteSpace(name))
            {
                playersQuery = playersQuery
                    .Where(p => p.FirstName.ToLower().Contains(name.ToLower()) 
                    || p.LastName.ToLower().Contains(name.ToLower()));
            }

            IEnumerable<PlayerViewModel> filteredPlayers = playersQuery
                .OrderBy(pvm => pvm.FirstName)
                .ThenBy(pvm => pvm.LastName)
                .ToList();

            return View(nameof(Index), filteredPlayers);
        }

        private List<CreatePlayerTeamViewModel> LoadTeamsDropdown()
        {
           List<CreatePlayerTeamViewModel> loadedTeams = _dbContext.Teams
                     .Select(t => new CreatePlayerTeamViewModel
                     {
                         Id = t.Id,
                         Name = t.Name
                     })
                     .OrderBy(t => t.Name)
                     .ToList();

            return loadedTeams;
        }

        private bool TeamExists(int teamId)
        {
            return _dbContext.Teams.Any(t => t.Id == teamId);
        }
    }
}
