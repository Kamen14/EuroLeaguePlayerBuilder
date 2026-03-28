using EuroLeaguePlayerBuilder.Services.Models.Teams;

namespace EuroLeaguePlayerBuilder.Services.Core.Interfaces
{
    public interface ITeamService
    {
        Task<IEnumerable<TeamDto>> GetAllTeamsAsync();

        Task<TeamDetailsDto> GetTeamDetailsByIdAsync(int id);

        Task<IEnumerable<HomePageTeamDto>> GetTeamsForHomePageAsync();
    }
}
