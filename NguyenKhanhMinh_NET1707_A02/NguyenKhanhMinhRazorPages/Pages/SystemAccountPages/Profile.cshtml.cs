using BusinessObjectsLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RepositoriesLayer;

namespace NguyenKhanhMinhRazorPages.Pages.SystemAccountPages
{
    public class ProfileModel : PageModel
    {
        private readonly ISystemAccountRepo _systemAccountRepo;

        public ProfileModel(ISystemAccountRepo systemAccountRepo)
        {
            _systemAccountRepo = systemAccountRepo;
        }

        [BindProperty]
        public SystemAccount SystemAccount { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string? returnUrl = "/NewsArticlePages/Index") // Default Page
        {
            var userEmail = HttpContext.Session.GetString("UserEmail");
            if (string.IsNullOrEmpty(userEmail))
            {
                return RedirectToPage("/NotPermission");
            }

            var currentUser = _systemAccountRepo.GetAccountByEmail(userEmail);
            SystemAccount = currentUser;

            ViewData["ReturnUrl"] = returnUrl; // Store returnUrl in ViewData
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string? returnUrl = "/NewsArticlePages/Index")
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                HttpContext.Session.SetString("UserName", SystemAccount.AccountName.ToString());
                _systemAccountRepo.UpdateAccount(SystemAccount);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (_systemAccountRepo.GetAccountById(SystemAccount.AccountId) == null)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage(returnUrl);
        }
    }
}
