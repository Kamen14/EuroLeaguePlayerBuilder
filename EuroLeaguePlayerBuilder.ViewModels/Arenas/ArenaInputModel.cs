using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using static EuroLeaguePlayerBuilder.GCommon.EntityValidation.Arena;
using static EuroLeaguePlayerBuilder.GCommon.ErrorMessages;

namespace EuroLeaguePlayerBuilder.ViewModels.Arenas
{
    public class ArenaInputModel
    {
        [Required]
        [StringLength(ArenaNameMaxLength, MinimumLength = ArenaNameMinLength, ErrorMessage = ArenaNameOutOfRange)]
        public string Name { get; set; } = null!;

        [Required]
        [StringLength(ArenaCityMaxLength, MinimumLength = ArenaCityMinLength, ErrorMessage = ArenaCityOutOfRange)]
        public string City { get; set; } = null!;

        [Required]
        [StringLength(ArenaCountryMaxLength, MinimumLength = ArenaCountryMinLength, ErrorMessage = ArenaCountryOutOfRange)]
        public string Country { get; set; } = null!;

        [Required]
        [Range(MinArenaCapacity, MaxArenaCapacity)]
        public int Capacity { get; set; }

        public IFormFile? Image { get; set; }
    }
}
