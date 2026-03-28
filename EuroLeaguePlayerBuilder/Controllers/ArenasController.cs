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
                ArenaInputDto inputDto = new ArenaInputDto
                {
                    Name = inputModel.Name,
                    City = inputModel.City,
                    Country = inputModel.Country,
                    Capacity = inputModel.Capacity,
                    Image = inputModel.Image
                };

                string? userId = GetUserId();
                string wwwRootPath = _webHostEnvironment.WebRootPath;

                await _arenaService.CreateArenaAsync(inputDto, userId!, wwwRootPath);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "An error occurred while creating the arena. Please try again.");

                return View(inputModel);
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
