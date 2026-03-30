using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EuroLeaguePlayerBuilder.Services.Models.Games
{
    public class GameInputDto
    {
        public int TeamOneId { get; set; }

        public int TeamTwoId { get; set; }

        public int ArenaId { get; set; }

        public IEnumerable<GameTeamDto> Teams { get; set; } = null!;

        public IEnumerable<GameArenaDto> Arenas { get; set; } = null!;
    }
}
