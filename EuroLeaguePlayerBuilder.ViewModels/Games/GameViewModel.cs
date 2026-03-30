namespace EuroLeaguePlayerBuilder.ViewModels.Games
{
    public class GameViewModel
    {
        public int Id { get; set; }

        //Team One
        public string TeamOneName { get; set; } = null!;

        public string TeamOneLogoPath { get; set; } = null!;

        public int TeamOneScore { get; set; }

        //Team One
        public string TeamTwoName { get; set; } = null!;

        public string TeamTwoLogoPath { get; set; } = null!;

        public int TeamTwoScore { get; set; }

        //Arena
        public string ArenaName { get; set; } = null!;
    }
}
