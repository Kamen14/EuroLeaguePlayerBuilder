using EuroLeaguePlayerBuilder.Services.Core.Interfaces;
using EuroLeaguePlayerBuilder.Services.Models.Arenas;
using EuroLeaguePlayerBuilder.Services.Models.Games;
using EuroLeaguePlayerBuilder.ViewModels.Arenas;
using EuroLeaguePlayerBuilder.ViewModels.Games;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EuroLeaguePlayerBuilder.Controllers
{
    [Authorize]
    public class GamesController : Controller
    {
        private readonly IGameService _gameService;
        public GamesController(IGameService gameService)
        {
            _gameService = gameService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IEnumerable<GameDto> games
                = await _gameService.GetAllGamesOrderedByTeamsNameAsync();
            IEnumerable<GameViewModel> gameViewModels = MapGameDtoToGameViewModel(games);

            return View(gameViewModels);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            GameInputDto gameInputData = await _gameService.GetGameInputDataAsync();

            IEnumerable<GameArenaViewModel> arenas = gameInputData.Arenas
                .Select(a => new GameArenaViewModel
                {
                    Id = a.Id,
                    Name = a.Name
                })
                .ToList();

            IEnumerable<GameTeamViewModel> teams = gameInputData.Teams
                .Select(t => new GameTeamViewModel
                {
                    Id = t.Id,
                    Name = t.Name,
                    LogoPath = t.LogoPath
                })
                .ToList();

            GameInputModel inputModel = new GameInputModel
            {
                Arenas = arenas,
                Teams = teams
            };

            return View(inputModel);
        }

        private static IEnumerable<GameViewModel> MapGameDtoToGameViewModel(IEnumerable<GameDto> games)
        {
            return games
                .Select(g => new GameViewModel
                {
                    Id = g.Id,
                    TeamOneName = g.TeamOneName,
                    TeamOneLogoPath = g.TeamOneLogoPath,
                    TeamOneScore = g.TeamOneScore,
                    TeamTwoName = g.TeamTwoName,
                    TeamTwoLogoPath = g.TeamTwoLogoPath,
                    TeamTwoScore = g.TeamTwoScore,
                    ArenaName = g.ArenaName
                });
        }
    }
}
