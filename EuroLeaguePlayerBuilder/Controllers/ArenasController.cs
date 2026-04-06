using EuroLeaguePlayerBuilder.Services.Core;
using EuroLeaguePlayerBuilder.Services.Core.Interfaces;
using EuroLeaguePlayerBuilder.Services.Models.Arenas;
using EuroLeaguePlayerBuilder.Services.Models.Players;
using EuroLeaguePlayerBuilder.ViewModels.Arenas;
using EuroLeaguePlayerBuilder.ViewModels.Players;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using static EuroLeaguePlayerBuilder.GCommon.ImageConstants.ArenaImages;

namespace EuroLeaguePlayerBuilder.Controllers
{
    [Authorize]
    public class ArenasController : Controller
    {
        private readonly IArenaService _arenaService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ArenasController(IArenaService arenaService, IWebHostEnvironment webHostEnvironment)
        {
            _arenaService = arenaService;
            _webHostEnvironment = webHostEnvironment;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IEnumerable<ArenaDto> arenas
                = await _arenaService.GetAllArenasOrderedByNameAsync();
            IEnumerable<ArenaViewModel> arenaViewModels = MapArenaDtoToArenaViewModel(arenas);

            return View(arenaViewModels);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ArenaInputModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                return View(inputModel);
            }

            try
            {
                ArenaInputDto inputDto = MapInputModelToDto(inputModel);

                string? userId = GetUserId();
                string wwwRootPath = _webHostEnvironment.WebRootPath;

                await _arenaService.CreateArenaAsync(inputDto, userId!, wwwRootPath);
                return RedirectToAction(nameof(Index));
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError("Image", ex.Message);
                return View(inputModel);
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "An error occurred while creating the arena. Please try again.");

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

            ArenaInputDto? arenaDto = await _arenaService.GetArenaInputModelWithLoadedDataAsync(id);

            if (arenaDto == null)
            {
                return NotFound();
            }

            if(!await _arenaService.IsArenaUserCreatedAsync(id))
            {
                return Forbid();
            }

            string? userId = GetUserId();
            if(!await _arenaService.IsArenaOwnedByUserAsync(id, userId!) && !User.IsInRole("Admin"))
            {
                return Forbid();
            }

            ArenaInputModel editModel = new ArenaInputModel
            {
                Name = arenaDto.Name,
                City = arenaDto.City,
                Country = arenaDto.Country,
                Capacity = arenaDto.Capacity,
            };

            ViewData[CurrentImagePathKey] = arenaDto.ImagePath;

            return View(editModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromRoute] int id, ArenaInputModel inputModel)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            if(!await _arenaService.ArenaExistsAsync(id))
            {
                return NotFound();
            }

            if (!await _arenaService.IsArenaUserCreatedAsync(id))
            {
                return Forbid();
            }

            string? userId = GetUserId();
            if (!await _arenaService.IsArenaOwnedByUserAsync(id, userId!) && !User.IsInRole("Admin"))
            {
                return Forbid();
            }

            if (!ModelState.IsValid)
            {
                return View(inputModel);
            }

            try
            {
                ArenaInputDto inputDto = MapInputModelToDto(inputModel);

                string wwwRootPath = _webHostEnvironment.WebRootPath;

                await _arenaService.EditArenaAsync(id, inputDto, wwwRootPath);

                return RedirectToAction(nameof(Index));
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError("Image", ex.Message);
                return View(inputModel);
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "An error occurred while editing the arena. Please try again.");
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

            DeleteArenaDto? deleteDto = await _arenaService.GetArenaForDeleteByIdAsync(id);
            if (deleteDto == null)
            {
                return NotFound();
            }

            if (!await _arenaService.IsArenaUserCreatedAsync(id))
            {
                return Forbid();
            }

            string? userId = GetUserId();
            if (!await _arenaService.IsArenaOwnedByUserAsync(id, userId!) && !User.IsInRole("Admin"))
            {
                return Forbid();
            }

            DeleteArenaViewModel deleteViewModel = new DeleteArenaViewModel
            {
                Name = deleteDto.Name,
                City = deleteDto.City
            };

            return View(deleteViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Delete([FromRoute] int id, DeleteArenaViewModel? deleteViewModel)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            if (!await _arenaService.ArenaExistsAsync(id))
            {
                return NotFound();
            }

            if (!await _arenaService.IsArenaUserCreatedAsync(id))
            {
                return Forbid();
            }

            string? userId = GetUserId();
            if (!await _arenaService.IsArenaOwnedByUserAsync(id, userId!) && !User.IsInRole("Admin"))
            {
                return Forbid();
            }

            try
            {
                await _arenaService.DeleteArenaAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "An error occurred while deleting the arena. Please try again.");
                return View(deleteViewModel);
            }
        }

        [HttpGet]
        public async Task<IActionResult> MyArenas()
        {
            string? userId = GetUserId();
            IEnumerable<ArenaDto> userArenas = await _arenaService
                .GetUserArenas(userId!);

            IEnumerable<ArenaViewModel> arenaViewModels
                = MapArenaDtoToArenaViewModel(userArenas);

            return View(arenaViewModels);
        }

        private static ArenaInputDto MapInputModelToDto(ArenaInputModel inputModel)
        {
            return new ArenaInputDto
            {
                Name = inputModel.Name,
                City = inputModel.City,
                Country = inputModel.Country,
                Capacity = inputModel.Capacity,
                Image = inputModel.Image
            };
        }

        private static IEnumerable<ArenaViewModel> MapArenaDtoToArenaViewModel(IEnumerable<ArenaDto> arenas)
        {
            return arenas.Select(a => new ArenaViewModel
            {
                Id = a.Id,
                Name = a.Name,
                City = a.City,
                Country = a.Country,
                Capacity = a.Capacity,
                ImagePath = a.ImagePath,
                UserId = a.UserId
            });
        }

        private string? GetUserId()
        {
            return User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }
    }
}
