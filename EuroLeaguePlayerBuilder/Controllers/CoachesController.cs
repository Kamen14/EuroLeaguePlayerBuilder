using EuroLeaguePlayerBuilder.Data;
using EuroLeaguePlayerBuilder.Services.Core;
using EuroLeaguePlayerBuilder.Services.Core.Interfaces;
using EuroLeaguePlayerBuilder.ViewModels.Coaches;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EuroLeaguePlayerBuilder.Controllers
{
    [AllowAnonymous]
    public class CoachesController : Controller
    {
        private readonly ICoachService _coachService;

        public CoachesController(ICoachService coachService)
        {
            _coachService = coachService;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<AllCoachesViewModel> coaches = await _coachService
                .GetAllCoachesWithTeamsAsync();

            return View(coaches);
        }
    }
}
