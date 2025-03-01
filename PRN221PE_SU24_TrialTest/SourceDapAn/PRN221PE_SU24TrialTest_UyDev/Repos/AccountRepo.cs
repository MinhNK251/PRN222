using BOs;
using DAOs;

namespace Repos
{
    public class AccountRepo : IAccountRepo
    {
        public async Task<SystemAccount> Login(string email, string password)
        {
            return await AccountDAO.Instance.Login(email, password);
        }
    }
}
