using EuroLeaguePlayerBuilder.ViewModels.Coaches;
using EuroLeaguePlayerBuilder.ViewModels.Players;
namespace EuroLeaguePlayerBuilder.ViewModels.Teams
{
    public class TeamDetailsViewModel
    {
        public string Name { get; set; } = null!;

        public string LogoPath { get; set; } = null!;

        public CoachViewModel Coach { get; set; } = null!;

        public IEnumerable<PlayerViewModel>? Players{ get; set; } = new List<PlayerViewModel>();

        public string ArenaName { get; set; } = null!;

        public int ArenaCapacity { get; set; }
    }
}
