using EuroLeaguePlayerBuilder.GCommon.Enums;
using EuroLeaguePlayerBuilder.Services.Core.Interfaces;
using EuroLeaguePlayerBuilder.Services.Models.Players;
using EuroLeaguePlayerBuilder.ViewModels.Players;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using static EuroLeaguePlayerBuilder.GCommon.ErrorMessages;

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
            IEnumerable<PlayerDto> allPlayers = await _playerService
                .GetAllPlayersOrderedByNameAsync();

            IEnumerable<PlayerViewModel> playerViewModels
                = MapPlayerDtoToPlayerViewModel(allPlayers);

            return View(playerViewModels);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            PlayerDetailsDto? detailsDto = await _playerService.GetPlayerDetailsByIdAsync(id);

            if (detailsDto == null)
            {
                return NotFound();
            }

            PlayerDetailsViewModel viewModel = new PlayerDetailsViewModel
            {
                FirstName = detailsDto.FirstName,
                LastName = detailsDto.LastName,
                Position = detailsDto.Position,
                PointsPerGame = detailsDto.PointsPerGame,
                ReboundsPerGame = detailsDto.ReboundsPerGame,
                AssistsPerGame = detailsDto.AssistsPerGame,
                TeamId = detailsDto.TeamId,
                TeamName = detailsDto.TeamName
            };

            return View(viewModel);
        }


        [HttpGet]
        public async Task<IActionResult> Create()
        {
            PlayerInputDto inputDto = await _playerService
                .GetPlayerInputModelWithLoadedTeamsAsync();

            PlayerInputModel inputModel = new PlayerInputModel
            {
                Teams = inputDto.Teams.Select(MapCreatePlayerTeamDtoToViewModel)
                .ToList()
            };

            return View(inputModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PlayerInputModel inputModel)
        {
            inputModel.Teams = (await _playerService.LoadTeamsDropdownAsync())
                .Select(MapCreatePlayerTeamDtoToViewModel)
                .ToList();

            if (!ModelState.IsValid)
            {
                return View(inputModel);
            }

            bool teamExists = inputModel.Teams
                .Any(t => t.Id == inputModel.TeamId);

            if (!teamExists)
            {
                ModelState.AddModelError(nameof(inputModel.TeamId), SelectedTeamDoesNotExistControllerError);

                return View(inputModel);
            }

            if (!Enum.IsDefined(typeof(Position), inputModel.Position))
            {
                ModelState.AddModelError(nameof(inputModel.Position), SelectedPositionIsInvalidControllerError);

                return View(inputModel);
            }

            try
            {
                string? userId = GetUserId();

                PlayerInputDto inputDto = new PlayerInputDto
                {
                    FirstName = inputModel.FirstName,
                    LastName = inputModel.LastName,
                    Position = inputModel.Position,
                    PointsPerGame = inputModel.PointsPerGame,
                    ReboundsPerGame = inputModel.ReboundsPerGame,
                    AssistsPerGame = inputModel.AssistsPerGame,
                    TeamId = inputModel.TeamId
                };

                await _playerService.CreatePlayerAsync(inputDto, userId!);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, PlayerCreationControllerError);

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

            PlayerInputDto? inputDto = await _playerService
                .GetPlayerInputModelWithLoadedTeamsAndPlayerDataAsync(id);

            if (inputDto == null)
            {
                return NotFound();
            }

            if (!await _playerService.IsPlayerUserCreatedAsync(id))
            {
                return Forbid();
            }
                
            string? userId = GetUserId();
            if (!await _playerService.IsPlayerOwnedByUserAsync(id, userId!) && !User.IsInRole("Admin"))
            {
                return Forbid();
            }

            PlayerInputModel inputModel = new PlayerInputModel
            {
                FirstName = inputDto.FirstName,
                LastName = inputDto.LastName,
                Position = inputDto.Position,
                PointsPerGame = inputDto.PointsPerGame,
                ReboundsPerGame = inputDto.ReboundsPerGame,
                AssistsPerGame = inputDto.AssistsPerGame,
                TeamId = inputDto.TeamId,
                Teams = inputDto.Teams
                .Select(MapCreatePlayerTeamDtoToViewModel)
                .ToList()
            };

            return View(inputModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
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

            if (!await _playerService.IsPlayerUserCreatedAsync(id))
            {
                return Forbid();
            }

            string? userId = GetUserId();
            if (!await _playerService.IsPlayerOwnedByUserAsync(id, userId!) && !User.IsInRole("Admin"))
            {
                return Forbid();
            }

            inputModel.Teams = (await _playerService.LoadTeamsDropdownAsync())
                .Select(MapCreatePlayerTeamDtoToViewModel)
                .ToList();

            if (!ModelState.IsValid)
            {
                return View(inputModel);
            }

            bool teamExists = inputModel.Teams
                .Any(t => t.Id == inputModel.TeamId);

            if (!teamExists)
            {
                ModelState.AddModelError(nameof(inputModel.TeamId), SelectedTeamDoesNotExistControllerError);

                return View(inputModel);
            }

            if (!Enum.IsDefined(typeof(Position), inputModel.Position))
            {
                ModelState.AddModelError(nameof(inputModel.Position), SelectedPositionIsInvalidControllerError);

                return View(inputModel);
            }

            try
            {
                PlayerInputDto inputDto = new PlayerInputDto
                {
                    FirstName = inputModel.FirstName,
                    LastName = inputModel.LastName,
                    Position = inputModel.Position,
                    PointsPerGame = inputModel.PointsPerGame,
                    ReboundsPerGame = inputModel.ReboundsPerGame,
                    AssistsPerGame = inputModel.AssistsPerGame,
                    TeamId = inputModel.TeamId,
                };

                await _playerService.EditPlayerAsync(id, inputDto);

                return RedirectToAction(nameof(Details), new { id });
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, PlayerEditControllerError);

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

            DeletePlayerDto deleteDto = await _playerService
                .GetPlayerForDeleteByIdAsync(id);

            if (deleteDto == null)
            {
                return NotFound();
            }

            if (!await _playerService.IsPlayerUserCreatedAsync(id))
            {
                return Forbid();
            }

            string? userId = GetUserId();
            if (!await _playerService.IsPlayerOwnedByUserAsync(id, userId!) && !User.IsInRole("Admin"))
            {
                return Forbid();
            }

            DeletePlayerViewModel deleteViewModel = new DeletePlayerViewModel
            {
                FirstName = deleteDto.FirstName,
                LastName = deleteDto.LastName
            };

            return View(deleteViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
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

            if (!await _playerService.IsPlayerUserCreatedAsync(id))
            {
                return Forbid();
            }

            string? userId = GetUserId();
            if (!await _playerService.IsPlayerOwnedByUserAsync(id, userId!) && !User.IsInRole("Admin"))
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
                ModelState.AddModelError(string.Empty, PlayerDeleteControllerError);
                return View(viewModel);
            }
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Search(string? name)
        {
            IEnumerable<PlayerDto> filteredPlayers = await _playerService
                .SearchPlayerByFirstAndLastNameAsync(name);

            IEnumerable<PlayerViewModel> playerViewModels
                = MapPlayerDtoToPlayerViewModel(filteredPlayers);

            return View(nameof(Index), playerViewModels);
        }

        public async Task<IActionResult> MyPlayers()
        {
            string? userId = GetUserId();
            IEnumerable<PlayerDto> userPlayers = await _playerService
                .GetUserPlayers(userId!);

            IEnumerable<PlayerViewModel> playerViewModels
                = MapPlayerDtoToPlayerViewModel(userPlayers);

            return View(playerViewModels);
        }

        private string? GetUserId()
        {
            return User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }

        private static IEnumerable<PlayerViewModel> MapPlayerDtoToPlayerViewModel(IEnumerable<PlayerDto> playerDtos)
        {
            return playerDtos
                .Select(dto => new PlayerViewModel
                {
                    Id = dto.Id,
                    FirstName = dto.FirstName,
                    LastName = dto.LastName,
                    Position = dto.Position,
                    UserId = dto.UserId
                });
        }

        private CreatePlayerTeamViewModel MapCreatePlayerTeamDtoToViewModel(CreatePlayerTeamDto dto)
        {
            return new CreatePlayerTeamViewModel
            {
                Id = dto.Id,
                Name = dto.Name
            };
        }
    }
}
       