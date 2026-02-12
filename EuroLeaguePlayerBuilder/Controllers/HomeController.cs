using EuroLeaguePlayerBuilder.Data;
using EuroLeaguePlayerBuilder.Data.Models;
using EuroLeaguePlayerBuilder.Services.Core.Interfaces;
using EuroLeaguePlayerBuilder.ViewModels;
using EuroLeaguePlayerBuilder.ViewModels.Home;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace EuroLeaguePlayerBuilder.Controllers
{
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
            IEnumerable<HomePageTeamViewModel> teams = await _teamService
                .GetTeamsForHomePageAsync();

            return View(teams);
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
