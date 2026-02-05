namespace EuroLeaguePlayerBuilder.ViewModels.Coaches
{
    public class CoachViewModel
    {
        public int Id { get; set; }

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public int TitlesWon { get; set; }  
    }
}
