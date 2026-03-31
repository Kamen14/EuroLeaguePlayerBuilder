using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EuroLeaguePlayerBuilder.Services.Models.Games
{
    public class GameDeleteDto
    {
        public string TeamOneName { get; set; } = null!;

        public string TeamTwoName { get; set; } = null!;
    }
}
