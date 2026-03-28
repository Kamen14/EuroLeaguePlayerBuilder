using Microsoft.AspNetCore.Http;

namespace EuroLeaguePlayerBuilder.Services.Models.Arenas
{
    public class ArenaInputDto
    {
        public string Name { get; set; } = null!;
        public string City { get; set; } = null!;
        public string Country { get; set; } = null!;

        public int Capacity { get; set; }

        public IFormFile? Image { get; set; }
    }
}
