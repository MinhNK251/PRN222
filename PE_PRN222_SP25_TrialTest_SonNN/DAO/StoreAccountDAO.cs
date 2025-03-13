using Sp25PharmaceuticalDB_BusinessObjects;

namespace Sp25PharmaceuticalDB_DAO
{
    public class StoreAccountDAO
    {
        private Sp25PharmaceuticalDbContext _dbContext;
        private static StoreAccountDAO instance;

        public StoreAccountDAO()
        {
            _dbContext = new Sp25PharmaceuticalDbContext(); 
        }

        public static StoreAccountDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new StoreAccountDAO();
                }
                return instance;
            }
        }

        public StoreAccount GetAccount(string email, string password)
        {
            return _dbContext.StoreAccounts.SingleOrDefault(m => m.EmailAddress.Equals(email)
                                                         && m.StoreAccountPassword.Equals(password));
        }
    }
}
