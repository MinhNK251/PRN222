using BOs;
using Microsoft.EntityFrameworkCore;

namespace DAOs
{
    public class AccountDAO
    {
        private readonly OilPaintingArt2024DBContext _context;
        private static AccountDAO instance = null;

        private AccountDAO()
        {
            _context = new OilPaintingArt2024DBContext();
        }

        public static AccountDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    return new AccountDAO();
                }
                return instance;
            }
        }

        public async Task<SystemAccount> Login(string email, string password)
        {
            return await _context.SystemAccounts.FirstOrDefaultAsync(acc => acc.AccountEmail == email && acc.AccountPassword == password);
        }
    }
}
