using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EuroLeaguePlayerBuilder.Services.Models.Games
{
    public class AdminGameDto
    {
        public int Id { get; set; }

        public string TeamOneLogoPath { get; set; } = null!;

        public int TeamOneScore { get; set; }

        public string TeamTwoLogoPath { get; set; } = null!;

        public int TeamTwoScore { get; set; }

        public string ArenaName { get; set; } = null!;

        public string? CreatedByEmail { get; set; }

        public string? CreatedByNickname { get; set; }
    }
}
