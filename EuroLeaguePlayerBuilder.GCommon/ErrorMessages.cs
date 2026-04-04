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
    }
}
