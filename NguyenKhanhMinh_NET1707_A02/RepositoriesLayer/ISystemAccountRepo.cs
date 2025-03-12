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
        SystemAccount? GetAccountById(short accountId);
        SystemAccount? GetAccountByEmail(string email);
        List<SystemAccount> GetAccounts();
        void AddAccount(SystemAccount account);
        void UpdateAccount(SystemAccount updatedAccount);
        void RemoveAccount(short accountId);
        SystemAccount? Login(string email, string password, IOptions<AdminAccountSettings> adminAccountSettings);
    }
}
