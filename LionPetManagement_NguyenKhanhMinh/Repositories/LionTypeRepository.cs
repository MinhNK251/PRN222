using BOs.Models;
using DAOs;

namespace Repositories
{
    public class LionTypeRepository : ILionTypeRepository
    {
        public List<LionType> GetLionTypes()
            => LionTypeDAO.Instance.GetLionTypess();
    }
}
