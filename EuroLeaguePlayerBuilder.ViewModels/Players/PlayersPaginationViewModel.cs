using static EuroLeaguePlayerBuilder.GCommon.GlobalConstants;
namespace EuroLeaguePlayerBuilder.ViewModels.Players
{
    public class PlayersPaginationViewModel
    {
        public string? SearchQuery { get; set; }

        public int PageNumber { get; set; } = 1;

        public int TotalPages { get; set; } = 1;

        public int ShowingPages { get; set; } = DefaultShowingPages;

        public int StartPageIndex { get; set; } = 1;

        public IEnumerable<PlayerViewModel> Players { get; set; } =
            new List<PlayerViewModel>();
    }
}
