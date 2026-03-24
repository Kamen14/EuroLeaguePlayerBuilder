namespace EuroLeaguePlayerBuilder.GCommon
{
    public class EntityValidation
    {
        public static class Team
        {
            public const int TeamNameMinLength = 2;
            public const int TeamNameMaxLength = 80;

            public const int TeamCountryMinLength = 2;
            public const int TeamCountryMaxLength = 70;

            public const int TeamCityMinLength = 2;
            public const int TeamCityMaxLength = 70;

        }
        public static class Coach
        {
            public const int CoachNameMinLength = 2;
            public const int CoachNameMaxLength = 40; //the same for both first and last name
        }
        public static class Player
        {
            public const int PlayerNameMinLength = 2;
            public const int PlayerNameMaxLength = 40;//the same for both first and last name

            public const int MinPointsPerGame = 0;
            public const int MaxPointsPerGame = 50;

            public const int MinReboundsPerGame = 0;
            public const int MaxReboundsPerGame = 30;

            public const int MinAssistsPerGame = 0;
            public const int MaxAssistsPerGame = 20;
        }

        public static class Arena
        {
            public const int ArenaNameMinLength = 2;
            public const int ArenaNameMaxLength = 100;

            public const int ArenaCityMinLength = 2;
            public const int ArenaCityMaxLength = 70;

            public const int ArenaCountryMinLength = 2;
            public const int ArenaCountryMaxLength = 70;

            public const int MinArenaCapacity = 1000;
            public const int MaxArenaCapacity = 50000;
        }
    }
}
