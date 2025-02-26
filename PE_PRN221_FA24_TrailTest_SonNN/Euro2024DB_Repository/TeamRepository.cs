using Euro2024DB_BusinessObjects;
using Euro2024DB_DAO;

namespace Euro2024DB_Repository
{
    public class TeamRepository : ITeamRepository
    {
        public void AddTeam(Team team)
            => TeamDAO.Instance.AddTeam(team);

        public Team GetTeamById(int? TeamId)
            => TeamDAO.Instance.GetTeamById(TeamId);

        public Team GetTeamByName(string teamName)
            => TeamDAO.Instance.GetTeamByName(teamName);

        public List<Team> GetTeams()
            => TeamDAO.Instance.GetTeams();

        public void RemoveTeam(int id)
            => TeamDAO.Instance.RemoveTeam(id);

        public void UpdateTeam(int teamId, Team team)
            => TeamDAO.Instance.UpdateTeam(teamId, team);
    }
}
