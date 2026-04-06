using EuroLeaguePlayerBuilder.Services.Core.Interfaces;
using EuroLeaguePlayerBuilder.Services.Models.Games;
using EuroLeaguePlayerBuilder.ViewModels.Games;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using static EuroLeaguePlayerBuilder.GCommon.ErrorMessages;

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
            string? userId = GetUserId();

            IEnumerable<GameDto> games
                = await _gameService.GetUserGamesAsync(userId!);
            IEnumerable<GameViewModel> gameViewModels = MapGameDtoToGameViewModel(games);

            return View(gameViewModels);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            GameInputDto gameInputData = await _gameService.GetGameInputDataAsync();

            IEnumerable<GameArenaViewModel> arenas = gameInputData.Arenas
                .Select(MapGameArenaDtoToViewModel())
                .ToList();

            IEnumerable<GameTeamViewModel> teams = gameInputData.Teams
                .Select(MapGameTeamDtoToViewModel())
                .ToList();

            GameInputModel inputModel = new GameInputModel
            {
                Arenas = arenas,
                Teams = teams
            };

            return View(inputModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(GameInputModel inputModel)
        {
            GameInputDto inputData = await _gameService.GetGameInputDataAsync();

            IEnumerable<GameArenaViewModel> arenas = inputData.Arenas
                .Select(MapGameArenaDtoToViewModel())
                .ToList();

            IEnumerable<GameTeamViewModel> teams = inputData.Teams
                .Select(MapGameTeamDtoToViewModel())
                .ToList();

            inputModel.Arenas = arenas;

            inputModel.Teams = teams;

            if (!ModelState.IsValid)
            {
                return View(inputModel);
            }

            if (inputModel.TeamOneId == inputModel.TeamTwoId)
            {
                ModelState.AddModelError(string.Empty, SameTeamsControllerError);
                return View(inputModel);
            }

            try
            {
                string? userId = GetUserId();

                GameInputDto inputDto = new GameInputDto
                {
                    TeamOneId = inputModel.TeamOneId,
                    TeamTwoId = inputModel.TeamTwoId,
                    ArenaId = inputModel.ArenaId
                };
                await _gameService.CreateGameAsync(inputDto, userId!);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError(string.Empty, GameCreationControllerError);
                return View(inputModel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Simulate([FromRoute] int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            if (!await _gameService.GameExistsAsync(id))
            {
                return NotFound();
            }

            string? userId = GetUserId();
            if (!await _gameService.IsGameOwnedByUserAsync(id, userId!) && !User.IsInRole("Admin"))
            {
                return Forbid();
            }

            Random random = new Random();
            int teamOneScore = random.Next(70, 111);
            int teamTwoScore = random.Next(70, 111);

            //prevents draws
            if(teamOneScore == teamTwoScore)
            {
                teamOneScore += 1;
            }

            await _gameService.UpdateGameScoreAsync(id, teamOneScore, teamTwoScore);

            return Json(new { teamOneScore, teamTwoScore });
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            GameDeleteDto? deleteDto = await _gameService.GetGameForDeleteByIdAsync(id);

            if(deleteDto == null)
            {
                return NotFound();
            }

            string? userId = GetUserId();
            if (!await _gameService.IsGameOwnedByUserAsync(id, userId!) && !User.IsInRole("Admin"))
            {
                return Forbid();
            }

            GameDeleteViewModel deleteViewModel = new GameDeleteViewModel
            {
                TeamOneName = deleteDto.TeamOneName,
                TeamTwoName = deleteDto.TeamTwoName
            };

            return View(deleteViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute] int id, GameDeleteViewModel? deleteViewModel)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            if (!await _gameService.GameExistsAsync(id))
            {
                return NotFound();
            }

            string? userId = GetUserId();
            if (!await _gameService.IsGameOwnedByUserAsync(id, userId!) && !User.IsInRole("Admin"))
            {
                return Forbid();
            }

            try
            {
                await _gameService.DeleteGameAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError(string.Empty, GameDeleteControllerError);
                return View(deleteViewModel);
            }
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
        private static Func<GameArenaDto, GameArenaViewModel> MapGameArenaDtoToViewModel()
        {
            return a => new GameArenaViewModel
            {
                Id = a.Id,
                Name = a.Name
            };
        }

        private static Func<GameTeamDto, GameTeamViewModel> MapGameTeamDtoToViewModel()
        {
            return t => new GameTeamViewModel
            {
                Id = t.Id,
                Name = t.Name,
                LogoPath = t.LogoPath
            };
        }

        private string? GetUserId()
        {
            return User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }
    }
}
