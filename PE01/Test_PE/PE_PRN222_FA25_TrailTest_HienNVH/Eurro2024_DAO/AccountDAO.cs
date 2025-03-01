using Euro2024_DAO;
using Euro2024DB_BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eurro2024_DAO
{
    public class AccountDAO
    {
        private Euro2024_DAO.Euro2024DBContext _dbContext; 
        private static AccountDAO instance;
        public AccountDAO() 
        {
            _dbContext = new Euro2024_DAO.Euro2024DBContext();
        
        }

        public static AccountDAO Instance { 
            get {
                if(instance == null)
                {
                    instance = new AccountDAO();
                }    
                return instance;
            } 
        }
        public Account GetAccount(string email, string password, string status)
        {
            return _dbContext.Accounts
                .SingleOrDefault(a => a.Email == email && a.Password == password && a.Status == status);
        }

    }
}
