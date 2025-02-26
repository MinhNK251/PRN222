using Euro2024DB_BusinessObjects;
using Microsoft.EntityFrameworkCore;

namespace Euro2024DB_Repository
{
    public interface ITeamRepository
    {
        public Team GetTeamById(int? TeamId);
        public Team GetTeamByName(String teamName);
        public List<Team> GetTeams();
        public void AddTeam(Team team);
        public void UpdateTeam(int teamId, Team team);

        public void RemoveTeam(int id);
    }
}
