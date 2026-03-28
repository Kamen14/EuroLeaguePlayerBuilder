namespace EuroLeaguePlayerBuilder.Services.Models.Teams
{
    public class TeamDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string City { get; set; } = null!;

        public string Country { get; set; } = null!;

        public string LogoPath { get; set; } = null!;

        public int PlayersCount { get; set; }
    }
}
