using BusinessObjectsLayer.DTOs;
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
        public async Task<SystemAccount?> Login(string email, string password, IOptions<AdminAccountSettings> adminAccountSettings)
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
                    AccountRole = 0 // Admin role
                };
            }

            using (var dbContext = CreateDbContext())
            {
                return await dbContext.SystemAccounts
                    .FirstOrDefaultAsync(acc => acc.AccountEmail == email && acc.AccountPassword == password);
            }
        }

        // Get Account by ID
        public async Task<SystemAccount?> GetAccountById(short accountId)
        {
            using (var dbContext = CreateDbContext())
            {
                return await dbContext.SystemAccounts.FindAsync(accountId);
            }
        }

        // Get All Accounts
        public async Task<List<SystemAccount>> GetAccounts()
        {
            using (var dbContext = CreateDbContext())
            {
                return await dbContext.SystemAccounts.ToListAsync();
            }
        }

        // Add New Account
        public async Task AddAccount(SystemAccount account)
        {
            using (var dbContext = CreateDbContext())
            {
                await dbContext.SystemAccounts.AddAsync(account);
                await dbContext.SaveChangesAsync();
            }
        }

        // Update Account
        public async Task UpdateAccount(SystemAccount account)
        {
            using (var dbContext = CreateDbContext())
            {
                dbContext.SystemAccounts.Update(account);
                await dbContext.SaveChangesAsync();
            }
        }

        // Remove Account by ID
        public async Task RemoveAccount(short accountId)
        {
            using (var dbContext = CreateDbContext())
            {
                var account = await GetAccountById(accountId);
                if (account != null)
                {
                    dbContext.SystemAccounts.Remove(account);
                    await dbContext.SaveChangesAsync();
                }
            }
        }
    }
}