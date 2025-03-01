using BusinessObjectsLayer.DTOs;
using BusinessObjectsLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer
{
    public interface ISystemAccountRepo
    {
        public Task<SystemAccount> Login(string email, string password);
        public Task<List<SystemAccount>> GetAllAccounts();
        public Task<SystemAccount> GetAccountById(int id);
        public Task AddAccount(SystemAccountDTO account);
        public Task UpdateAccount(SystemAccountDTO account);
        public Task DeleteAccount(int id);
    }
}
