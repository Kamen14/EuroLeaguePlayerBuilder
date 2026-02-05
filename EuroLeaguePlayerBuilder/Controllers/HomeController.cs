using EuroLeaguePlayerBuilder.Data;
using EuroLeaguePlayerBuilder.Models;
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
        private readonly ApplicationDbContext _dbContext;

        public HomeController(ApplicationDbContext dbContext, ILogger<HomeController> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public IActionResult Index()
        {
            IEnumerable<HomePageTeamViewModel> teams = _dbContext
                .Teams
                .Select(t => new HomePageTeamViewModel
                {
                    Id = t.Id,
                    Name = t.Name,
                    LogoPath = t.LogoPath
                })
                .AsNoTracking()
                .ToList();

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
