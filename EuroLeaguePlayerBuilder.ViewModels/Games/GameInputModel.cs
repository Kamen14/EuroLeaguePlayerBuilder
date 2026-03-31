using System.ComponentModel.DataAnnotations;

namespace EuroLeaguePlayerBuilder.ViewModels.Games
{
    public class GameInputModel
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Please Select Team One.")]
        public int TeamOneId { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Please Select Team Two.")]
        public int TeamTwoId { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Please select an Arena.")]
        public int ArenaId { get; set; }

        public IEnumerable<GameTeamViewModel> Teams { get; set; } 
            = new List<GameTeamViewModel>();

        public IEnumerable<GameArenaViewModel> Arenas { get; set; } 
            = new List<GameArenaViewModel>();
    }
}
