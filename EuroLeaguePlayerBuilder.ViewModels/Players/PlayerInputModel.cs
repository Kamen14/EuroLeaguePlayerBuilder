using EuroLeaguePlayerBuilder.GCommon.Enums;
using System.ComponentModel.DataAnnotations;
using static EuroLeaguePlayerBuilder.GCommon.EntityValidation.Player;

namespace EuroLeaguePlayerBuilder.ViewModels.Players
{
    public class PlayerInputModel
    {
        [Required]
        [MinLength(PlayerNameMinLength)]
        [MaxLength(PlayerNameMaxLength)]
        public string FirstName { get; set; } = null!;

        [Required]
        [MinLength(PlayerNameMinLength)]
        [MaxLength(PlayerNameMaxLength)]
        public string LastName { get; set; } = null!;

        [Required]
        public Position Position { get; set; }

        [Required]
        [Range(MinPointsPerGame, MaxPointsPerGame)]
        public double PointsPerGame { get; set; }

        [Required]
        [Range(MinReboundsPerGame, MaxReboundsPerGame)]
        public double ReboundsPerGame { get; set; }

        [Required]
        [Range(MinAssistsPerGame, MaxAssistsPerGame)]
        public double AssistsPerGame { get; set; }

        [Required]
        [Display(Name = "Team")]
        public int TeamId { get; set; }

        public IEnumerable<CreatePlayerTeamViewModel> Teams { get; set; } 
            = new List<CreatePlayerTeamViewModel>();
    }
}
