using BusinessObjectsLayer.DTOs;
using BusinessObjectsLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace DAOsLayer
{
    public class SystemAccountDAO
    {
        private readonly FunewsManagementContext _context;
        private readonly AdminAccountSettings _adminAccountSettings;

        // Public constructor for dependency injection
        public SystemAccountDAO(FunewsManagementContext context, IOptions<AdminAccountSettings> adminAccountSettings)
        {
            _context = context;
            _adminAccountSettings = adminAccountSettings.Value;
        }

        // Login method for authentication
        public async Task<SystemAccount> Login(string email, string password)
        {
            // Check if the provided credentials match the admin account
            if (email == _adminAccountSettings.Email && password == _adminAccountSettings.Password)
            {
                return new SystemAccount
                {
                    AccountId = 0, // Admin ID
                    AccountName = "Admin",
                    AccountEmail = _adminAccountSettings.Email,
                    AccountPassword = _adminAccountSettings.Password,
                    AccountRole = 0 // Admin role
                };
            }

            // Otherwise, check the database for other accounts
            return await _context.SystemAccounts
                .FirstOrDefaultAsync(acc => acc.AccountEmail == email && acc.AccountPassword == password);
        }

        // Get all accounts (for Admin)
        public async Task<List<SystemAccount>> GetAllAccounts()
        {
            return await _context.SystemAccounts.ToListAsync();
        }

        // Get account by ID (for Admin)
        public async Task<SystemAccount> GetAccountById(int id)
        {
            return await _context.SystemAccounts
                         .Include(sa => sa.NewsArticleCreatedBies) // Include created articles
                         .Include(sa => sa.NewsArticleUpdatedBies) // Include updated articles
                         .FirstOrDefaultAsync(sa => sa.AccountId == id);
        }

        // Add a new account (for Admin)
        public async Task AddAccount(SystemAccountDTO accountDTO)
        {
            try
            {
                var existingAccount = await _context.SystemAccounts
                                                   .FirstOrDefaultAsync(a => a.AccountId == accountDTO.AccountId);

                if (existingAccount != null)
                {
                    throw new Exception("Account already exists");
                }

                var account = new SystemAccount
                {
                    AccountName = accountDTO.AccountName,
                    AccountEmail = accountDTO.AccountEmail,
                    AccountPassword = accountDTO.AccountPassword, // Ensure password is hashed before saving
                    AccountRole = accountDTO.AccountRole
                };

                _context.SystemAccounts.Add(account);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while adding the account.", ex);
            }
        }

        // Update an existing account
        public async Task UpdateAccount(SystemAccountDTO accountDTO)
        {
            try
            {
                var existingAccount = await _context.SystemAccounts
                                                   .FirstOrDefaultAsync(a => a.AccountId == accountDTO.AccountId);

                if (existingAccount == null)
                {
                    throw new Exception("Account not found");
                }

                existingAccount.AccountName = accountDTO.AccountName;
                existingAccount.AccountEmail = accountDTO.AccountEmail;
                existingAccount.AccountPassword = accountDTO.AccountPassword;
                existingAccount.AccountRole = accountDTO.AccountRole;

                _context.SystemAccounts.Update(existingAccount);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating the account.", ex);
            }
        }

        // Delete an account (for Admin)
        public async Task DeleteAccount(int id)
        {
            var account = await GetAccountById(id);
            if (account == null)
            {
                throw new Exception("Account not found");
            }

            _context.SystemAccounts.Remove(account);
            await _context.SaveChangesAsync();
        }
    }
}