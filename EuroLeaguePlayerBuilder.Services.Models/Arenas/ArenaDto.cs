using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EuroLeaguePlayerBuilder.Services.Models.Arenas
{
    public class ArenaDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public string City { get; set; } = null!;

        public string Country { get; set; } = null!;

        public int Capacity { get; set; }

        public string? ImagePath { get; set; }

        public string? UserId { get; set; }
    }
}
