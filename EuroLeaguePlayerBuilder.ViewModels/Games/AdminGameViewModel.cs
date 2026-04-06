namespace EuroLeaguePlayerBuilder.ViewModels.Games
{
    public class AdminGameViewModel
    {
        public int Id { get; set; }

        public string TeamOneLogoPath { get; set; } = null!;

        public int TeamOneScore { get; set; }

        public string TeamTwoLogoPath { get; set; } = null!;

        public int TeamTwoScore { get; set; }

        public string ArenaName { get; set; } = null!;

        public string? CreatedByEmail { get; set; }

        public string? CreatedByNickname { get; set; }
    }
}
