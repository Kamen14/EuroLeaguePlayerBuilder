namespace EuroLeaguePlayerBuilder.ViewModels.Players
{
    public class PlayerViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Position { get; set; } = null!;
    }
}
