using BusinessObjectsLayer.DTOs;
using BusinessObjectsLayer.Models;
using DAOsLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer
{
    public class SystemAccountRepo : ISystemAccountRepo
    {
        private readonly SystemAccountDAO _systemAccountDAO;

        // Public constructor for dependency injection
        public SystemAccountRepo(SystemAccountDAO systemAccountDAO)
        {
            _systemAccountDAO = systemAccountDAO;
        }

        public Task<SystemAccount> Login(string email, string password)
        {
            return _systemAccountDAO.Login(email, password);
        }

        public Task<List<SystemAccount>> GetAllAccounts()
        {
            return _systemAccountDAO.GetAllAccounts();
        }

        public Task<SystemAccount> GetAccountById(int id)
        {
            return _systemAccountDAO.GetAccountById(id);
        }

        public Task AddAccount(SystemAccountDTO account)
        {
            return _systemAccountDAO.AddAccount(account);
        }

        public Task UpdateAccount(SystemAccountDTO account)
        {
            return _systemAccountDAO.UpdateAccount(account);
        }

        public Task DeleteAccount(int id)
        {
            return _systemAccountDAO.DeleteAccount(id);
        }
    }
}
