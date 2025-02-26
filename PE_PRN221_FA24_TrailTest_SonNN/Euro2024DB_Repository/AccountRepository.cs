using Euro2024DB_BusinessObjects;
using Euro2024DB_DAO;

namespace Euro2024DB_Repository
{
    public class AccountRepository : IAccountRepository
    {
        public Account GetAccount(string email, string password, string status)
            =>AccountDAO.Instance.GetAccount(email, password, status); 
    }
}
