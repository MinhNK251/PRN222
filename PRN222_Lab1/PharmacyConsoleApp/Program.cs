using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PharmacyConsoleApp.Models;
using System.IO;

namespace PharmacyConsoleApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            // Load configuration
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            string connectionString = configuration.GetConnectionString("DefaultConnection");
            Console.WriteLine($"Connection String: {connectionString}");

            // Set up DbContext options
            var optionsBuilder = new DbContextOptionsBuilder<Lab1PharmaceuticalDbContext>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));

            using (var context = new Lab1PharmaceuticalDbContext(optionsBuilder.Options))
            {
                // 1. Read: Display all StoreAccounts
                Console.WriteLine("Store Accounts:");
                var accounts = context.StoreAccounts.ToList();
                foreach (var account in accounts)
                {
                    Console.WriteLine($"ID: {account.StoreAccountId}, Email: {account.EmailAddress}, Role: {account.Role}");
                }

                // 2. Create: Add a new StoreAccount
                var newAccount = new StoreAccount
                {
                    StoreAccountId = 555,
                    StoreAccountPassword = "@4",
                    EmailAddress = "newmember@PharmaceuticalStore.com",
                    StoreAccountDescription = "New Member",
                    Role = 4
                };
                context.StoreAccounts.Add(newAccount);
                context.SaveChanges();
                Console.WriteLine("New account added.");

                // 3. Update: Update an existing StoreAccount
                var accountToUpdate = context.StoreAccounts.FirstOrDefault(a => a.StoreAccountId == 555);
                if (accountToUpdate != null)
                {
                    accountToUpdate.StoreAccountDescription = "Updated Member";
                    context.SaveChanges();
                    Console.WriteLine("Account updated.");
                }

                // 4. Delete: Delete the new StoreAccount
                if (accountToUpdate != null)
                {
                    context.StoreAccounts.Remove(accountToUpdate);
                    context.SaveChanges();
                    Console.WriteLine("Account deleted.");
                }

                // 5. Read: Display all MedicineInformation with Manufacturer
                Console.WriteLine("\nMedicine Information:");
                var medicines = context.MedicineInformations
                    .Include(m => m.Manufacturer)
                    .ToList();
                foreach (var medicine in medicines)
                {
                    Console.WriteLine($"ID: {medicine.MedicineId}, Name: {medicine.MedicineName}, Manufacturer: {medicine.Manufacturer?.ManufacturerName}");
                }

                // 6. Read: Display only medicines with `ExpirationDate` containing "3 years"
                var filteredMedicines = context.MedicineInformations
                    .Where(m => m.ExpirationDate.Contains("3 years"))
                    .ToList();
                Console.WriteLine("\nMedicines with 3-year expiration:");
                foreach (var medicine in filteredMedicines)
                {
                    Console.WriteLine($"ID: {medicine.MedicineId}, Name: {medicine.MedicineName}");
                }

                // 7. Read: Test Validate ActiveIngredients
                Console.WriteLine("\nTest Validate ActiveIngredients:");
                Console.WriteLine($"Is Acetylsalicylic Acid valid? {IsValidActiveIngredients("Acetylsalicylic Acid")}");
                Console.WriteLine($"Is acetylsalicylic acid valid? {IsValidActiveIngredients("acetylsalicylic acid")}");
            }
        }

        // Validate ActiveIngredients
        static bool IsValidActiveIngredients(string ingredients)
        {
            if (string.IsNullOrEmpty(ingredients) || ingredients.Length <= 10)
                return false;

            if (ingredients.Any(c => "#@&()".Contains(c)))
                return false;

            var words = ingredients.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            return words.All(word => char.IsUpper(word[0]) || char.IsDigit(word[0]));
        }
    }
}
