namespace EuroLeaguePlayerBuilder.ViewModels.Coaches
{
    public class AllCoachesViewModel
    {
        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public int TitlesWon { get; set; }

        public int TeamId { get; set; }

        public string TeamLogoPath { get; set; } = null!;
    }
}
