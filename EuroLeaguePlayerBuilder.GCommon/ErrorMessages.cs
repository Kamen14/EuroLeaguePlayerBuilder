using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EuroLeaguePlayerBuilder.GCommon
{
    public static class ErrorMessages
    {
        public const string PlayerNameOutOfRange = "Name must be between 2 and 40 characters long.";

        public const string ArenaNameOutOfRange = "Name must be between 2 and 100 characters long.";
        public const string ArenaCityOutOfRange = "Name must be between 2 and 70 characters long.";
        public const string ArenaCountryOutOfRange = "Name must be between 2 and 70 characters long.";

        //ArenaController 
        public const string ArenaCreationControllerError = "An error occurred while creating the arena. Please try again.";
        public const string ArenaEditControllerError = "An error occurred while editing the arena. Please try again.";
        public const string ArenaDeleteControllerError = "An error occurred while deleting the arena. Please try again.";

        //GamesController
        public const string SameTeamsControllerError = "Team One and Team Two cannot be the same team.";
        public const string GameCreationControllerError = "An error occurred while creating the game. Please try again.";
        public const string GameDeleteControllerError = "An error occurred while deleting the game. Please try again.";

        //PlayersController
        public const string SelectedTeamDoesNotExistControllerError = "Selected team does not exist.";
        public const string SelectedPositionIsInvalidControllerError = "Selected position is invalid.";
        public const string PlayerCreationControllerError = "An error occurred while creating the player. Please try again.";
        public const string PlayerEditControllerError = "An error occured while editing the player. Please try again.";
        public const string PlayerDeleteControllerError = "An error occurred while deleting the player. Please try again.";

        //ArenaService
        public const string InvalidFileTypeServiceError = "Invalid file type.";
        public const string FileSizeServiceError = "File size must not exceed 3MB.";
        public const string ArenaAddToDatabaseServiceError = "An error occurred while adding the arena to the database.";
        public const string ArenaWithThisIdDoesNotExistServiceError = "Arena with the provided ID does not exist.";

        //GameService 
        public const string GameAddToDatabaseServiceError = "An error occurred while adding the game to the database.";
        public const string GameDoesNotExistServiceError = "The specified game does not exist.";

        //PlayerService 
        public const string PlayerAddToDatabaseServiceError = "An error occurred while adding the player to the database.";
        public const string PlayerWithProvidedIdDoesNotExistServiceError = "Player with the provided ID does not exist.";

        //UserService
        public const string UserNotFoundServiceError = "User not found.";
    }
}
