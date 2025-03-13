using Sp25PharmaceuticalDB_BusinessObjects;

namespace Sp25PharmaceuticalDB_Repository
{
    public interface IStoreAccountRepository
    {
        public StoreAccount GetAccount(string email, string password);
    }
}
