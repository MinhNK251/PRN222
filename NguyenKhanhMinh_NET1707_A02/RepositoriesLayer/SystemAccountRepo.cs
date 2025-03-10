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

namespace RepositoriesLayer
{
    public class SystemAccountRepo : ISystemAccountRepo
    {
        public async Task<SystemAccount?> GetAccountById(short accountId)
            => await SystemAccountDAO.Instance.GetAccountById(accountId);

        public async Task<List<SystemAccount>> GetAccounts()
            => await SystemAccountDAO.Instance.GetAccounts();

        public async Task AddAccount(SystemAccount account)
            => await SystemAccountDAO.Instance.AddAccount(account);

        public async Task UpdateAccount(SystemAccount updatedAccount)
            => await SystemAccountDAO.Instance.UpdateAccount(updatedAccount);

        public async Task RemoveAccount(short accountId)
            => await SystemAccountDAO.Instance.RemoveAccount(accountId);

        public async Task<SystemAccount?> Login(string email, string password, IOptions<AdminAccountSettings> adminAccountSettings)
            => await SystemAccountDAO.Instance.Login(email, password, adminAccountSettings);
    }
}
