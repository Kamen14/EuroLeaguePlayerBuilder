namespace EuroLeaguePlayerBuilder.Services.Models.Players
{
    public class PlayerDetailsDto
    {
        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string Position { get; set; } = null!;

        public double PointsPerGame { get; set; }

        public double ReboundsPerGame { get; set; }

        public double AssistsPerGame { get; set; }

        public int TeamId { get; set; }

        public string TeamName { get; set; } = null!;
    }
}
