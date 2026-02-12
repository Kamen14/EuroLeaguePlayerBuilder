using EuroLeaguePlayerBuilder.ViewModels.Home;
using EuroLeaguePlayerBuilder.ViewModels.Teams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EuroLeaguePlayerBuilder.Services.Core.Interfaces
{
    public interface ITeamService
    {
        Task<IEnumerable<TeamViewModel>> GetAllTeamsAsync();

        Task<TeamDetailsViewModel> GetTeamDetailsByIdAsync(int id);

        Task<IEnumerable<HomePageTeamViewModel>> GetTeamsForHomePageAsync();
    }
}
