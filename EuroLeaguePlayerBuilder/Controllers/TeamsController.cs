using EuroLeaguePlayerBuilder.Data;
using EuroLeaguePlayerBuilder.Data.Models;
using EuroLeaguePlayerBuilder.ViewModels.Teams;
using EuroLeaguePlayerBuilder.ViewModels.Players;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EuroLeaguePlayerBuilder.ViewModels.Coaches;
using static EuroLeaguePlayerBuilder.GCommon.PlayerPositionHelper;
using EuroLeaguePlayerBuilder.Services.Core.Interfaces;
using EuroLeaguePlayerBuilder.Services.Core;

namespace EuroLeaguePlayerBuilder.Controllers
{
    public class TeamsController : Controller
    {
        private readonly ITeamService _teamService;
        public TeamsController(ApplicationDbContext dbContext, ITeamService teamService)
        {
            _teamService = teamService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IEnumerable<TeamViewModel> allTeams = await _teamService
                .GetAllTeamsAsync();

            return View(allTeams);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            if(id <= 0)
            {
                return BadRequest();
            }

            TeamDetailsViewModel teamDetails = await _teamService
                .GetTeamDetailsByIdAsync(id);

            if (teamDetails == null)
            {
                return NotFound();
            }

            return View(teamDetails);
        }
    }
}
