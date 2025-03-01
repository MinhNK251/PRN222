using BOs;

namespace Repos
{
    public interface IAccountRepo
    {
        Task<SystemAccount> Login(string email, string password);
    }
}
