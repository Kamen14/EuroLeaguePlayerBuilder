using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EuroLeaguePlayerBuilder.Services.Models.Coaches
{
    public class AllCoachesDto
    {
        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public int TitlesWon { get; set; }

        public int TeamId { get; set; }

        public string TeamLogoPath { get; set; } = null!;
    }
}
