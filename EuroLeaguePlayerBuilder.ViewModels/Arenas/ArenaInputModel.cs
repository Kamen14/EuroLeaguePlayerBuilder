using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using static EuroLeaguePlayerBuilder.GCommon.EntityValidation.Arena;

namespace EuroLeaguePlayerBuilder.ViewModels.Arenas
{
    public class ArenaInputModel
    {
        [Required]
        [MinLength(ArenaNameMinLength)]
        [MaxLength(ArenaNameMaxLength)]
        public string Name { get; set; } = null!;

        [Required]
        [MinLength(ArenaCityMinLength)]
        [MaxLength(ArenaCityMaxLength)]
        public string City { get; set; } = null!;

        [Required]
        [MinLength(ArenaCountryMinLength)]
        [MaxLength(ArenaCountryMaxLength)]
        public string Country { get; set; } = null!;

        [Required]
        [Range(MinArenaCapacity, MaxArenaCapacity)]
        public int Capacity { get; set; }

        public IFormFile? Image { get; set; }
    }
}
