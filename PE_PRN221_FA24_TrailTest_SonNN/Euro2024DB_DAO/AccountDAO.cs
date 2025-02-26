using Euro2024DB_BusinessObjects;

namespace Euro2024DB_DAO
{
    public class AccountDAO
    {
        private Euro2024DbContext _dbContext;
        private static AccountDAO instance;

        public AccountDAO()
        {
            _dbContext = new Euro2024DbContext();
        }

        public static AccountDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new AccountDAO();
                }
                return instance;
            }
        }

        public Account GetAccount(string email, string password, string status)
        {
            return _dbContext.Accounts.SingleOrDefault(m => m.Email.Equals(email) 
                                                         && m.Password.Equals(password) 
                                                         && m.Status.Equals(status));
        }
    }
}
