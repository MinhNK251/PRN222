using Euro2024DB_BusinessObjects;

namespace Euro2024DB_Repository
{
    public interface IAccountRepository
    {
        public Account GetAccount(string email, string password, string status);
    }
}
