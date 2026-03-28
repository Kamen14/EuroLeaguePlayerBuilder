using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EuroLeaguePlayerBuilder.ViewModels.Arenas
{
    public class ArenaViewModel
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
