using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EuroLeaguePlayerBuilder.GCommon
{
    public  class ImageConstants
    {
        public static class TeamImages
        {
            public const string PanathinaikosLogo = "/images/teams/panathinaikos.svg.png";
            public const string OlympiacosLogo = "/images/teams/olympiacos.svg.png";
            public const string RealMadridLogo = "/images/teams/real_madrid.svg.png";
            public const string BarcelonaLogo = "/images/teams/barcelona.svg.png";
            public const string CrvenaZvezdaLogo = "/images/teams/crvena_zvezda.svg.png";
            public const string PartizanLogo = "/images/teams/partizan.svg.png";
        }

        public static class ArenaImages
        {
            public const string OakaImage = "/images/arenas/oaka.jpg";
            public const string PeaceAndFriendshipStadiumImage = "/images/arenas/peace_and_friendship_stadium.jpg";
            public const string WiZinkCenterImage = "/images/arenas/wizink_center.jpg";
            public const string PalauBlaugranaImage = "/images/arenas/palau_blaugrana.jpg";
            public const string BelgradeArenaImage = "/images/arenas/belgrade_arena.jpg";
            public const string DefaultArenaImage = "/images/arenas/default_arena.jpg";

            public const long MaxArenaImageSize = 3 * 1024 * 1024; // 3MB

            public const string CurrentImagePathKey = "CurrentImagePath";
        }


    }
}
