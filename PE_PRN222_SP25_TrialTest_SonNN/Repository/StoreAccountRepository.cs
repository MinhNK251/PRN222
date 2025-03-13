using Sp25PharmaceuticalDB_BusinessObjects;
using Sp25PharmaceuticalDB_DAO;

namespace Sp25PharmaceuticalDB_Repository
{
    public class StoreAccountRepository : IStoreAccountRepository
    {
        public StoreAccount GetAccount(string email, string password)
            => StoreAccountDAO.Instance.GetAccount(email, password);
    }
}
