using Euro2024DB_BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Euro2024_Repository
{
    public interface IAccountRepo
    {
        public Account GetAccount(string email, string password, string status);
    }
}
