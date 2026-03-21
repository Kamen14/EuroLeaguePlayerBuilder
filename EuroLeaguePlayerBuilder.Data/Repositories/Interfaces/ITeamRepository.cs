using EuroLeaguePlayerBuilder.Data.Models;

namespace EuroLeaguePlayerBuilder.Data.Repositories.Interfaces
{
    public interface ITeamRepository
    {
        IQueryable<Team> GetAllTeamsWithPlayersNoTracking();

        Task<Team?> GetTeamWithPlayersAndCoachByIdNoTrackingAsync(int id);

        IQueryable<Team> GetAllTeamsNoTracking();
    }
}
