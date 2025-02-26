using Euro2024DB_BusinessObjects;
using Microsoft.EntityFrameworkCore;

namespace Euro2024DB_DAO
{
    public class TeamDAO
    {
        private Euro2024DbContext _dbContext;
        private static TeamDAO instance;

        public TeamDAO()
        {
            _dbContext = new Euro2024DbContext();
        }

        public static TeamDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new TeamDAO();
                }
                return instance;
            }
        }

        public Team GetTeamById(int? TeamId)
        {
            return _dbContext.Teams.SingleOrDefault(t => t.Id.Equals(TeamId));
        }

        public Team GetTeamByName(String teamName)
        {
            return _dbContext.Teams.SingleOrDefault(t => t.TeamName.Equals(teamName));
        }

        public List<Team> GetTeams()
        {
            return _dbContext.Teams.Include(team => team.Group).ToList();
        }

        public void AddTeam(Team team)
        {
            Team currentTeam = GetTeamByName(team.TeamName);
            if (currentTeam == null) 
            {
                _dbContext.Teams.Add(team);
                _dbContext.SaveChanges();
            }
        }

        public void UpdateTeam(int teamId, Team team)
        {
            Team currentTeam = GetTeamById(teamId);
            if (currentTeam != null)
            {
                _dbContext.Teams.Attach(team);
                _dbContext.Entry(team).State = EntityState.Modified;
                _dbContext.SaveChanges();
            }
        }

        public void RemoveTeam(int id)
        {
            Team currentTeam = GetTeamById(id);
            if (currentTeam != null) 
            {
                _dbContext.Teams.Remove(currentTeam);
                _dbContext.SaveChanges();
            }
        }
    }
}
