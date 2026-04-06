using EuroLeaguePlayerBuilder.Services.Core.Interfaces;
using EuroLeaguePlayerBuilder.Services.Models.Arenas;
using EuroLeaguePlayerBuilder.Services.Models.Games;
using EuroLeaguePlayerBuilder.Services.Models.Players;
using EuroLeaguePlayerBuilder.ViewModels.Admin;
using Microsoft.AspNetCore.Mvc;

namespace EuroLeaguePlayerBuilder.Areas.Admin.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IPlayerService _playerService;
        private readonly IArenaService _arenaService;
        private readonly IGameService _gameService;

        public HomeController(
            IPlayerService playerService, IArenaService arenaService, 
            IGameService gameService)
        {
            _playerService = playerService;
            _arenaService = arenaService;
            _gameService = gameService;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<PlayerDto> players = await _playerService.GetAllPlayersOrderedByNameAsync();
            IEnumerable<ArenaDto> arenas = await _arenaService.GetAllArenasOrderedByNameAsync();
            IEnumerable<GameDto> games = await _gameService.GetAllGamesOrderedByTeamsNameAsync();

            AdminDashboardViewModel viewModel = new AdminDashboardViewModel()
            {
                TotalPlayers = players.Count(),
                TotalArenas = arenas.Count(),
                TotalGames = games.Count()
            };

            return View(viewModel);
        }
    }
}
