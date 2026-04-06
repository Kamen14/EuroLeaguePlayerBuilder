using EuroLeaguePlayerBuilder.Services.Core.Interfaces;
using EuroLeaguePlayerBuilder.Services.Models.Games;
using EuroLeaguePlayerBuilder.ViewModels.Games;
using Microsoft.AspNetCore.Mvc;

namespace EuroLeaguePlayerBuilder.Areas.Admin.Controllers
{
    public class GamesController : BaseController
    {
        private readonly IGameService _gameService;

        public GamesController(IGameService gameService)
        {
            _gameService = gameService;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<AdminGameDto> dtos = await _gameService
                .GetAllArenasForAdminAsync();

            IEnumerable<AdminGameViewModel> adminGameViewModels = dtos
                .Select(dto => new AdminGameViewModel
                {
                    Id = dto.Id,
                    TeamOneLogoPath = dto.TeamOneLogoPath,
                    TeamOneScore = dto.TeamOneScore,
                    TeamTwoLogoPath = dto.TeamTwoLogoPath,
                    TeamTwoScore = dto.TeamTwoScore,
                    ArenaName = dto.ArenaName,
                    CreatedByEmail = dto.CreatedByEmail,
                    CreatedByNickname = dto.CreatedByNickname,
                })
                .ToList();

            return View(adminGameViewModels);
        }
    }
}
