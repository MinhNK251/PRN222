using BOs.Models;

namespace Repositories
{
    public interface ILionAccountRepository
    {
        public LionAccount GetAccount(string email, string password);
    }
}
