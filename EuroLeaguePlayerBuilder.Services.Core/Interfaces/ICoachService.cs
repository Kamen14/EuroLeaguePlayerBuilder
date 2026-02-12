using EuroLeaguePlayerBuilder.Data;
using EuroLeaguePlayerBuilder.ViewModels.Coaches;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EuroLeaguePlayerBuilder.Services.Core.Interfaces
{
    public interface ICoachService
    {
        Task<IEnumerable<AllCoachesViewModel>> GetAllCoachesWithTeamsAsync();
    }
}
