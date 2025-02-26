using Euro2024DB_BusinessObjects;
using Microsoft.EntityFrameworkCore;

namespace Euro2024DB_DAO
{
    public class GroupTeamDAO
    {
        private Euro2024DbContext _dbContext;
        private static GroupTeamDAO instance;

        public GroupTeamDAO()
        {
            _dbContext = new Euro2024DbContext();
        }

        public static GroupTeamDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GroupTeamDAO();
                }
                return instance;
            }
        }

        public List<GroupTeam> GetTeams()
        {
            return _dbContext.GroupTeams.ToList();
        }
    }
}
