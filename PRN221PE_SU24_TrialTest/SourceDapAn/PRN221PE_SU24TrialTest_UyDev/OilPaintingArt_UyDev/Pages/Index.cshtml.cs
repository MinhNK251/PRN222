using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repos;

namespace OilPaintingArt_UyDev.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public string email { get; set; }

        [BindProperty]
        public string password { get; set; }

        private readonly IAccountRepo _accountRepo;

        public IndexModel(IAccountRepo accountRepo)
        {
            _accountRepo = accountRepo;
        }

        public async Task<IActionResult> OnPost()
        {
            try
            {
                var account = await _accountRepo.Login(email, password);
                if (account != null && (account.Role == 2 || account.Role == 3))
                {
                    TempData["Message"] = "Login Success";
                    Console.WriteLine("Login Success");

                    //set session
                    HttpContext.Session.SetString("Email", email);
                    HttpContext.Session.SetInt32("RoleId", account.Role ?? default(int));

                    return RedirectToPage("/OilPaintingArtPage/Index");
                }
                else
                {
                    TempData["Message"] = "You do not have permission to do this function";
                    return Page();
                }
            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
                return Page();
            }
        }
    }
}
