using EuroLeaguePlayerBuilder.Services.Models.Arenas;
using EuroLeaguePlayerBuilder.Services.Models.Games;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EuroLeaguePlayerBuilder.Services.Core.Interfaces
{
    public interface IGameService
    {
        Task<IEnumerable<GameDto>> GetAllGamesOrderedByTeamsNameAsync();

        Task<GameInputDto> GetGameInputDataAsync();
    }
}
