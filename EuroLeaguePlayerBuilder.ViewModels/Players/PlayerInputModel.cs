using EuroLeaguePlayerBuilder.GCommon.Enums;
using System.ComponentModel.DataAnnotations;
using static EuroLeaguePlayerBuilder.GCommon.EntityValidation.Player;
using static EuroLeaguePlayerBuilder.GCommon.ErrorMessages;

namespace EuroLeaguePlayerBuilder.ViewModels.Players
{
    public class PlayerInputModel
    {
        [Required]
        [StringLength(PlayerNameMaxLength, MinimumLength = PlayerNameMinLength, ErrorMessage = PlayerNameOutOfRange)]
        public string FirstName { get; set; } = null!;

        [Required]
        [StringLength(PlayerNameMaxLength, MinimumLength = PlayerNameMinLength, ErrorMessage = PlayerNameOutOfRange)]
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
