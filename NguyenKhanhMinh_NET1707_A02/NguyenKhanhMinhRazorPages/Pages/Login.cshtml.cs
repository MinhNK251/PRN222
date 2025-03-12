using BusinessObjectsLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using RepositoriesLayer;

namespace NguyenKhanhMinhRazorPages.Pages
{
    public class LoginModel : PageModel
    {
        private readonly ISystemAccountRepo _accountRepo;
        private readonly IOptions<AdminAccountSettings> _adminAccountSettings;

        public LoginModel(ISystemAccountRepo accountRepo, IOptions<AdminAccountSettings> adminAccountSettings)
        {
            _accountRepo = accountRepo;
            _adminAccountSettings = adminAccountSettings;
        }

        [BindProperty]
        public string Email { get; set; } = string.Empty;

        [BindProperty]
        public string Password { get; set; } = string.Empty;

        public string? ErrorMessage { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password))
            {
                ErrorMessage = "Email and password are required.";
                return Page();
            }

            var account = _accountRepo.Login(Email, Password, _adminAccountSettings);

            if (account != null)
            {
                // Store session data
                HttpContext.Session.SetString("UserEmail", account.AccountEmail);
                HttpContext.Session.SetString("UserRole", account.AccountRole.ToString());
                HttpContext.Session.SetString("UserName", account.AccountName.ToString());
                return account.AccountRole == 0
                    ? RedirectToPage("/SystemAccountPages/Index")
                    : RedirectToPage("/NewsArticlePages/Index");
            }

            // Failed login
            ErrorMessage = "Invalid email or password.";
            return Page();
        }
    }
}
