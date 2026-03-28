using EuroLeaguePlayerBuilder.Services.Core.Interfaces;
using EuroLeaguePlayerBuilder.Services.Models.Teams;
using EuroLeaguePlayerBuilder.ViewModels;
using EuroLeaguePlayerBuilder.ViewModels.Home;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EuroLeaguePlayerBuilder.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ITeamService _teamService;

        public HomeController(ILogger<HomeController> logger, ITeamService teamService)
        {
            _logger = logger;
            _teamService = teamService;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<HomePageTeamDto> teams = await _teamService
                .GetTeamsForHomePageAsync();

            IEnumerable<HomePageTeamViewModel> homePageTeams = teams
                .Select(t => new HomePageTeamViewModel
                {
                    Id = t.Id,
                    Name = t.Name,
                    LogoPath = t.LogoPath
                })
                .ToList();

            return View(homePageTeams);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
