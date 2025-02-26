using Euro2024DB_BusinessObjects;
using Euro2024DB_DAO;

namespace Euro2024DB_Repository
{
    public class GroupTeamRepository : IGroupTeamRepository
    {
        public List<GroupTeam> GetTeams()
            => GroupTeamDAO.Instance.GetTeams();
    }
}
