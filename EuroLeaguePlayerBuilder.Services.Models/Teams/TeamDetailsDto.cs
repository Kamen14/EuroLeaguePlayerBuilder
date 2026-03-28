using EuroLeaguePlayerBuilder.Services.Models.Coaches;
using EuroLeaguePlayerBuilder.Services.Models.Players;

namespace EuroLeaguePlayerBuilder.Services.Models.Teams
{
    public class TeamDetailsDto
    {
        public string Name { get; set; } = null!;

        public string LogoPath { get; set; } = null!;

        public CoachDto Coach { get; set; } = null!;

        public IEnumerable<PlayerDto>? Players { get; set; } = new List<PlayerDto>();
    }
}
