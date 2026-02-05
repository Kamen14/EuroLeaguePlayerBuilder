namespace EuroLeaguePlayerBuilder.ViewModels.Teams
{
    public class TeamViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string City { get; set; } = null!;

        public string Country { get; set; } = null!;

        public string LogoPath { get; set; } = null!;

        public int PlayersCount { get; set; }
    }
}
