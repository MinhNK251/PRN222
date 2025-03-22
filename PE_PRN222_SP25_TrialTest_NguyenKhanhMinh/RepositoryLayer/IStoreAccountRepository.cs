using BusinessObjectLayer;

namespace RepositoryLayer
{
    public interface IStoreAccountRepository
    {
        public StoreAccount GetAccount(string email, string password);
    }
}
