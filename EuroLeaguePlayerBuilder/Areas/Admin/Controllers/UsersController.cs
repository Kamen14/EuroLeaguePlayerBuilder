using EuroLeaguePlayerBuilder.Data.Models;
using EuroLeaguePlayerBuilder.Services.Core.Interfaces;
using EuroLeaguePlayerBuilder.Services.Models.Users;
using EuroLeaguePlayerBuilder.ViewModels.Admin;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EuroLeaguePlayerBuilder.Areas.Admin.Controllers
{
    public class UsersController : BaseController
    {
        private readonly IUserService _userService;
        private readonly UserManager<ApplicationUser> _userManager;

        public UsersController(IUserService userService, UserManager<ApplicationUser> userManager)
        {
            _userService = userService;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<UserDto> userDtos = await _userService.GetAllUsersWithRolesAsync();

            IEnumerable<AdminManageUserViewModel> userViewModels = userDtos
                .Select(dto => new AdminManageUserViewModel
                {
                    Id = dto.Id,
                    Email = dto.Email,
                    Nickname = dto.Nickname,
                    Role = dto.Role,
                });

            return View(userViewModels);
        }

        public async Task<IActionResult> PromoteToAdmin(string id)
        {
            if (id == _userManager.GetUserId(User))
            {
                return Forbid();
            }

            await _userService.PromoteToAdminAsync(id);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> DemoteToUser(string id)
        {
            if (id == _userManager.GetUserId(User))
            {
                return Forbid();
            }

            await _userService.DemoteToUserAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
