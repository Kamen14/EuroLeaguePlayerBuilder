using EuroLeaguePlayerBuilder.GCommon.Enums;
using System.ComponentModel.DataAnnotations;

namespace EuroLeaguePlayerBuilder.Services.Models.Players
{
    public class PlayerInputDto
    {
        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public Position Position { get; set; } 

        public double PointsPerGame { get; set; }

        public double ReboundsPerGame { get; set; }

        public double AssistsPerGame { get; set; }

        public int TeamId { get; set; }

        public IEnumerable<CreatePlayerTeamDto> Teams { get; set; }
            = new List<CreatePlayerTeamDto>();
    }
}
