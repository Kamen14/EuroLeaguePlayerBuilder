namespace EuroLeaguePlayerBuilder.Common
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
            public const int CoachNameMaxLength = 150; //the same for both first and last name
        }
        public static class Player
        {
            public const int PlayerNameMinLength = 2;
            public const int PlayerNameMaxLength = 150; //the same for both first and last name

            public const int PlayerPositionMaxLength = 20;
        }
    }
}
