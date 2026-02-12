using EuroLeaguePlayerBuilder.GCommon.Enums;

namespace EuroLeaguePlayerBuilder.GCommon
{
    public static class PlayerPositionHelper
    {

        public static Dictionary<Position, string> PositionToString { get; } = new Dictionary<Position, string>
        {
            { Position.PointGuard, "Point Guard" },
            { Position.ShootingGuard, "Shooting Guard" },
            { Position.SmallForward, "Small Forward" },
            { Position.PowerForward, "Power Forward" },
            { Position.Center, "Center" }
        };
    }
}
