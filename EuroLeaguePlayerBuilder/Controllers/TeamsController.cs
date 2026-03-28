using EuroLeaguePlayerBuilder.Services.Core.Interfaces;
using EuroLeaguePlayerBuilder.Services.Models.Teams;
using EuroLeaguePlayerBuilder.ViewModels.Coaches;
using EuroLeaguePlayerBuilder.ViewModels.Players;
using EuroLeaguePlayerBuilder.ViewModels.Teams;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EuroLeaguePlayerBuilder.Controllers
{
    [Authorize]
    public class TeamsController : Controller
    {
        private readonly ITeamService _teamService;
        public TeamsController(ITeamService teamService)
        {
            _teamService = teamService;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IEnumerable<TeamDto> allTeams = await _teamService
                .GetAllTeamsAsync();

            IEnumerable<TeamViewModel> teamViewModels = allTeams
                .Select(t => new TeamViewModel
            {
                Id = t.Id,
                Name = t.Name,
                City = t.City,
                Country = t.Country,
                LogoPath = t.LogoPath,
                PlayersCount = t.PlayersCount
            });

            return View(teamViewModels);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            if(id <= 0)
            {
                return BadRequest();
            }

            TeamDetailsDto teamDetails = await _teamService
                .GetTeamDetailsByIdAsync(id);

            if (teamDetails == null)
            {
                return NotFound();
            }

            TeamDetailsViewModel teamDetailsViewModel = new TeamDetailsViewModel
            {
                Name = teamDetails.Name,
                LogoPath = teamDetails.LogoPath,
                Coach = new CoachViewModel
                {
                    Id = teamDetails.Coach.Id,
                    FirstName = teamDetails.Coach.FirstName,
                    LastName = teamDetails.Coach.LastName,
                    TitlesWon = teamDetails.Coach.TitlesWon
                },
                Players = teamDetails.Players!.Select(p => new PlayerViewModel
                {
                    Id = p.Id,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    Position = p.Position,
                    UserId = p.UserId,
                })
               .OrderBy(pvm => pvm.FirstName)
               .ToList()
            };

            return View(teamDetailsViewModel);
        }
    }
}
