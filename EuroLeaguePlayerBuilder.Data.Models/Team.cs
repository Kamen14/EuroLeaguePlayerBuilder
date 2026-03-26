using System.ComponentModel.DataAnnotations;
using static EuroLeaguePlayerBuilder.GCommon.EntityValidation.Team;

namespace EuroLeaguePlayerBuilder.Data.Models
{
    public class Team
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(TeamNameMaxLength)]
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(TeamCountryMaxLength)]
        public string Country { get; set; } = null!;

        [Required]
        [MaxLength(TeamCityMaxLength)]
        public string City { get; set; } = null!;

        [Required]
        public string LogoPath { get; set; } = null!;   

        [Required]
        public int CoachId { get; set; }

        public virtual Coach Coach { get; set; } = null!;

        public int? ArenaId { get; set; }

        public virtual Arena? Arena { get; set; } = null!;

        public virtual ICollection<Player> Players { get; set; } = new List<Player>();
    }
}
