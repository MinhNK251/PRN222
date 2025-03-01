using Euro2024DB_BusinessObjects;
using Eurro2024_DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Euro2024_Repository
{
    public  interface AccountRepo : IAccountRepo
    {
        public Account GetAccount(string email, string password, string status)=>AccountDAO.Instance.GetAccount(email, password, status);

    }
}
