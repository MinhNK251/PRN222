using BusinessObjectsLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAOsLayer
{
    using BusinessObjectsLayer.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Options;
    using System.Linq;
    using System.Threading.Tasks;

    public class SystemAccountDAO
    {
        private static SystemAccountDAO? instance;
        private readonly FunewsManagementContext _dbContext;

        public SystemAccountDAO()
        {
            _dbContext = new FunewsManagementContext();
        }

        public static SystemAccountDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SystemAccountDAO();
                }
                return instance;
            }
        }

        private FunewsManagementContext CreateDbContext()
        {
            return new FunewsManagementContext();
        }

        // Login function (Admin & Regular Users)
        public SystemAccount? Login(string email, string password, IOptions<AdminAccountSettings> adminAccountSettings)
        {
            var adminSettings = adminAccountSettings.Value;

            // Check if the provided credentials match the admin account
            if (email == adminSettings.Email && password == adminSettings.Password)
            {
                return new SystemAccount
                {
                    AccountId = 0, // Admin ID
                    AccountName = "Admin",
                    AccountEmail = adminSettings.Email,
                    AccountPassword = adminSettings.Password,
                    AccountRole = adminSettings.Role,
                };
            }

            using (var dbContext = CreateDbContext())
            {
                return dbContext.SystemAccounts.AsNoTracking()
                    .FirstOrDefault(acc => acc.AccountEmail == email && acc.AccountPassword == password);
            }
        }

        // Get Account by Email
        public SystemAccount? GetAccountByEmail(string email)
        {
            using (var dbContext = CreateDbContext())
            {
                return dbContext.SystemAccounts.AsNoTracking()
                    .FirstOrDefault(acc => acc.AccountEmail == email);
            }
        }

        // Get Account by ID
        public SystemAccount? GetAccountById(short accountId)
        {
            using (var dbContext = CreateDbContext())
            {
                return dbContext.SystemAccounts.AsNoTracking()
                    .Include(acc => acc.NewsArticles)
                    .SingleOrDefault(a => a.AccountId == accountId);
            }
        }

        // Get All Accounts
        public List<SystemAccount> GetAccounts()
        {
            using (var dbContext = CreateDbContext())
            {
                return dbContext.SystemAccounts.AsNoTracking().ToList();
            }
        }

        // Add New Account
        public void AddAccount(SystemAccount account)
        {
            using (var dbContext = CreateDbContext())
            {
                dbContext.SystemAccounts.Add(account);
                dbContext.SaveChanges();
            }
        }

        // Update Account
        public void UpdateAccount(SystemAccount account)
        {
            using (var dbContext = CreateDbContext())
            {
                dbContext.SystemAccounts.Update(account);
                dbContext.SaveChanges();
            }
        }

        // Remove Account by ID
        public void RemoveAccount(short accountId)
        {
            using (var dbContext = CreateDbContext())
            {
                var account = GetAccountById(accountId);
                if (account != null)
                {
                    dbContext.SystemAccounts.Remove(account);
                    dbContext.SaveChanges();
                }
            }
        }
    }
}