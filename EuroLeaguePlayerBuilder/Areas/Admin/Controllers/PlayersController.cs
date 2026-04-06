using EuroLeaguePlayerBuilder.Data.Repositories.Interfaces;
using EuroLeaguePlayerBuilder.Services.Core.Interfaces;
using EuroLeaguePlayerBuilder.Services.Models.Players;
using EuroLeaguePlayerBuilder.ViewModels.Players;
using Microsoft.AspNetCore.Mvc;

namespace EuroLeaguePlayerBuilder.Areas.Admin.Controllers
{
    public class PlayersController : BaseController
    {
        private IPlayerService _playerService;
        public PlayersController(IPlayerService playerService)
        {
            _playerService = playerService;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<AdminPlayerDto> dtos = await _playerService
                .GetAllPlayersForAdminAsync();

            IEnumerable<AdminPlayerViewModel> adminPlayerViewModels = dtos
                .Select(dto => new AdminPlayerViewModel
                {
                    Id = dto.Id,
                    FirstName = dto.FirstName,
                    LastName = dto.LastName,
                    Position = dto.Position,
                    CreatedByEmail = dto.CreatedByEmail,
                    CreatedByNickname = dto.CreatedByNickname,
                })
                .ToList();

            return View(adminPlayerViewModels);
        }
    }
}
