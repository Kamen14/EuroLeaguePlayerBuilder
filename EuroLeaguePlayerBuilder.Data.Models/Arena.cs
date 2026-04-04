using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using static EuroLeaguePlayerBuilder.GCommon.EntityValidation.Arena;


namespace EuroLeaguePlayerBuilder.Data.Models
{
    public class Arena
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(ArenaNameMaxLength)]
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(ArenaCityMaxLength)]
        public string City { get; set; } = null!;

        [Required]
        [MaxLength(ArenaCountryMaxLength)]
        public string Country { get; set; } = null!;

        [Required]
        public int Capacity { get; set; }

        public string? ImagePath { get; set; }

        public string? UserId { get; set; }

        public ApplicationUser? User { get; set; }
    }
}
