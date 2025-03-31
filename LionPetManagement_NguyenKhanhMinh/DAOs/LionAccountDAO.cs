using BOs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAOs
{
    public class LionAccountDAO
    {
        private Sp25lionDbContext _dbContext;
        private static LionAccountDAO instance;

        public LionAccountDAO()
        {
            _dbContext = new Sp25lionDbContext();
        }

        public static LionAccountDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new LionAccountDAO();
                }
                return instance;
            }
        }

        public LionAccount GetAccount(string email, string password)
        {
            return _dbContext.LionAccounts.SingleOrDefault(m => m.Email.Equals(email)
                                                         && m.Password.Equals(password));
        }
    }
}
