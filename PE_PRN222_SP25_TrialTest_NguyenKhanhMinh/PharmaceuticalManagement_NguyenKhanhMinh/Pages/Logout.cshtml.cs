using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PharmaceuticalManagement_NguyenKhanhMinh.Pages
{
    public class LogoutModel : PageModel
    {
        public IActionResult OnGet()
        {
            // Clear session data
            HttpContext.Session.Clear();

            // Redirect to login page
            return RedirectToPage("/Login");
        }
    }
}
