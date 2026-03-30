namespace EuroLeaguePlayerBuilder.ViewModels.Games
{
    public class GameInputModel
    {
        public int TeamOneId { get; set; }

        public int TeamTwoId { get; set; }

        public int ArenaId { get; set; }

        public IEnumerable<GameTeamViewModel> Teams { get; set; } = null!;

        public IEnumerable<GameArenaViewModel> Arenas { get; set; } = null!;
    }
}
