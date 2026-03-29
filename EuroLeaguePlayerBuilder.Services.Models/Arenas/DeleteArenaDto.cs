using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EuroLeaguePlayerBuilder.Services.Models.Arenas
{
    public class DeleteArenaDto
    {
        public string Name { get; set; } = null!;

        public string City { get; set; } = null!;
    }
}
