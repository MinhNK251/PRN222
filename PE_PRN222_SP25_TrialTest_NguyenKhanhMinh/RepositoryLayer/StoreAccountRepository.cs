using BusinessObjectLayer;
using DataAccessLayer;

namespace RepositoryLayer
{
    public class StoreAccountRepository : IStoreAccountRepository
    {
        public StoreAccount GetAccount(string email, string password)
            => StoreAccountDAO.Instance.GetAccount(email, password);
    }
}
