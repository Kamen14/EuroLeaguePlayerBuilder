using EuroLeaguePlayerBuilder.Services.Models.Coaches;

namespace EuroLeaguePlayerBuilder.Services.Core.Interfaces
{
    public interface ICoachService
    {
        Task<IEnumerable<AllCoachesDto>> GetAllCoachesWithTeamsAsync();
    }
}
