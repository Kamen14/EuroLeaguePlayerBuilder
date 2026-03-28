namespace EuroLeaguePlayerBuilder.Services.Models.Players
{
    public class PlayerDto
    {
        public int Id { get; set; }

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string Position { get; set; } = null!;

        public string? UserId { get; set; }
    }
}
