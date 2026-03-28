using EuroLeaguePlayerBuilder.Services.Core.Interfaces;
using EuroLeaguePlayerBuilder.Services.Models.Coaches;
using EuroLeaguePlayerBuilder.ViewModels.Coaches;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
            IEnumerable<AllCoachesDto> coaches = await _coachService
                .GetAllCoachesWithTeamsAsync();

            IEnumerable<AllCoachesViewModel> coachesViewModel = coaches.Select(c => new AllCoachesViewModel
            {
                FirstName = c.FirstName,
                LastName = c.LastName,
                TitlesWon = c.TitlesWon,
                TeamId = c.TeamId,
                TeamLogoPath = c.TeamLogoPath
            });

            return View(coachesViewModel);
        }
    }
}
