using EuroLeaguePlayerBuilder.GCommon.Enums;
using EuroLeaguePlayerBuilder.Services.Core.Interfaces;
using EuroLeaguePlayerBuilder.ViewModels.Players;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using static EuroLeaguePlayerBuilder.GCommon.PlayerPositionHelper;

namespace EuroLeaguePlayerBuilder.Controllers
{
    [Authorize]
    public class PlayersController : Controller
    {
        private readonly IPlayerService _playerService;

        public PlayersController(IPlayerService playerService)
        {
            _playerService = playerService;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IEnumerable<PlayerViewModel> allPlayers = await _playerService
                .GetAllPlayersOrderedByNameAsync();

            return View(allPlayers);
        }

        public async Task<IActionResult> Details(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            PlayerDetailsViewModel? viewModel = await _playerService.GetPlayerDetailsByIdAsync(id);

            if (viewModel == null)
            {
                return NotFound();
            }

            return View(viewModel);
        }


        [HttpGet]
        public async Task<IActionResult> Create()
        {
            PlayerInputModel inputModel = await _playerService
                .GetPlayerInputModelWithLoadedTeamsAsync();

            return View(inputModel);
        }


        [HttpPost]
        public async Task<IActionResult> Create(PlayerInputModel inputModel)
        {
            inputModel.Teams = await _playerService.LoadTeamsDropdownAsync();

            if (!ModelState.IsValid)
            {
                return View(inputModel);
            }

            bool teamExists = inputModel.Teams
                .Any(t => t.Id == inputModel.TeamId);

            if (!teamExists)
            {
                ModelState.AddModelError(nameof(inputModel.TeamId), "Selected team does not exist.");

                return View(inputModel);
            }

            if (!Enum.IsDefined(typeof(Position), inputModel.Position))
            {
                ModelState.AddModelError(nameof(inputModel.Position), "Selected position is invalid.");

                return View(inputModel);
            }

            try
            {
                string? userId = GetUserId();

                await _playerService.CreatePlayerAsync(inputModel, userId!);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while creating the player. Please try again.");

                return View(inputModel);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            PlayerInputModel? inputModel = await _playerService
                .GetPlayerInputModelWithLoadedTeamsAndPlayerDataAsync(id);

            if (inputModel == null)
            {
                return NotFound();
            }

            string? userId = GetUserId();
            if (!await _playerService.IsPlayerOwnedByUserAsync(id, userId!))
            {
                return Forbid();
            }

            return View(inputModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromRoute] int id, PlayerInputModel inputModel)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            bool playerExists = await _playerService
                .PlayerExistsAsync(id);

            if (!playerExists)
            {
                return NotFound();
            }

            string? userId = GetUserId();
            if (!await _playerService.IsPlayerOwnedByUserAsync(id, userId!))
            {
                return Forbid();
            }

            inputModel.Teams = await _playerService.LoadTeamsDropdownAsync();

            if (!ModelState.IsValid)
            {
                return View(inputModel);
            }

            bool teamExists = inputModel.Teams
                .Any(t => t.Id == inputModel.TeamId);

            if (!teamExists)
            {
                ModelState.AddModelError(nameof(inputModel.TeamId), "Selected team does not exist.");

                return View(inputModel);
            }

            if (!Enum.IsDefined(typeof(Position), inputModel.Position))
            {
                ModelState.AddModelError(nameof(inputModel.Position), "Selected position is invalid.");

                return View(inputModel);
            }

            try
            {
                await _playerService.EditPlayerAsync(id, inputModel);

                return RedirectToAction(nameof(Details), new { id });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "An error occured while editing the player. Please try again.");

                return View(inputModel);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            DeletePlayerViewModel deleteViewModel = await _playerService
                .GetPlayerForDeleteByIdAsync(id);

            if (deleteViewModel == null)
            {
                return NotFound();
            }

            string? userId = GetUserId();
            if (!await _playerService.IsPlayerOwnedByUserAsync(id, userId!))
            {
                return Forbid();
            }

            return View(deleteViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Delete([FromRoute] int id, DeletePlayerViewModel? viewModel, string? returnUrl)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            bool playerExists = await _playerService.
                PlayerExistsAsync(id);

            if (!playerExists)
            {
                return NotFound();
            }

            string? userId = GetUserId();
            if (!await _playerService.IsPlayerOwnedByUserAsync(id, userId!))
            {
                return Forbid();
            }

            try
            {
                await _playerService.DeletePlayerAsync(id);

                if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "An error occurred while deleting the player. Please try again.");
                return View(viewModel);
            }
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Search(string? name)
        {
            IEnumerable<PlayerViewModel> filteredPlayers = await _playerService
                .SearchPlayerByFirstAndLastNameAsync(name);

            return View(nameof(Index), filteredPlayers);
        }

        public async Task<IActionResult> MyPlayers()
        {
            string? userId = GetUserId();
            IEnumerable<PlayerViewModel> userPlayers = await _playerService
                .GetUsersPlayers(userId!);

            return View(userPlayers);
        }

        private string? GetUserId()
        {
            return User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }
    }
}
