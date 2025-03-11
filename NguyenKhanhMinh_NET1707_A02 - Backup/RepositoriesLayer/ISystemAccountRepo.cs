using BusinessObjectsLayer.DTOs;
using BusinessObjectsLayer.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoriesLayer
{
    public interface ISystemAccountRepo
    {
        Task<SystemAccount?> GetAccountById(short accountId);
        Task<SystemAccount?> GetAccountByEmail(string email);
        Task<List<SystemAccount>> GetAccounts();
        Task AddAccount(SystemAccount account);
        Task UpdateAccount(SystemAccount updatedAccount);
        Task RemoveAccount(short accountId);
        Task<SystemAccount?> Login(string email, string password, IOptions<AdminAccountSettings> adminAccountSettings);
    }
}
