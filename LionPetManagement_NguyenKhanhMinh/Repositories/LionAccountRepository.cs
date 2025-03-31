using BOs.Models;
using DAOs;

namespace Repositories
{
    public class LionAccountRepository : ILionAccountRepository
    {
        public LionAccount GetAccount(string email, string password)
            => LionAccountDAO.Instance.GetAccount(email, password);
    }
}
