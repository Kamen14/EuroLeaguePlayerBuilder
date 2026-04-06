using EuroLeaguePlayerBuilder.Services.Core.Interfaces;
using EuroLeaguePlayerBuilder.Services.Models.Arenas;
using EuroLeaguePlayerBuilder.ViewModels.Arenas;
using Microsoft.AspNetCore.Mvc;

namespace EuroLeaguePlayerBuilder.Areas.Admin.Controllers
{
    public class ArenasController : BaseController
    {
        private readonly IArenaService _arenaService;

        public ArenasController(IArenaService arenaService)
        {
            _arenaService = arenaService;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<AdminArenaDto> dtos = await _arenaService
                .GetAllArenasForAdminAsync();

            IEnumerable<AdminArenaViewModel> adminArenaViewModels = dtos
                .Select(dto => new AdminArenaViewModel
                {
                    Id = dto.Id,
                    Name = dto.Name,
                    City = dto.City,
                    Country = dto.Country,
                    Capacity = dto.Capacity,
                    CreatedByEmail = dto.CreatedByEmail,
                    CreatedByNickname = dto.CreatedByNickname,
                })
                .ToList();

            return View(adminArenaViewModels);
        }
    }
}
