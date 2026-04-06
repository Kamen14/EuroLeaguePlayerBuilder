using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EuroLeaguePlayerBuilder.ViewModels.Admin
{
    public class AdminManageUserViewModel
    {
        public string Id { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string? Nickname { get; set; }

        public string Role { get; set; } = null!;
    }
}
