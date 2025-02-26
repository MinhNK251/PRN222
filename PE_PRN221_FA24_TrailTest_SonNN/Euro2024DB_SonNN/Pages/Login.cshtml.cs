using Euro2024DB_BusinessObjects;
using Euro2024DB_Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Euro2024DB_SonNN.Pages
{
    public class LoginModel : PageModel
    {
        private IAccountRepository _accountRepository;

        public LoginModel(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }
        public void OnGet()
        {
        }

        public void OnPost() 
        {
            String txtEmail = Request.Form["txtEmail"];
            String txtPassword = Request.Form["txtPassword"];
            Account account = _accountRepository.GetAccount(txtEmail, txtPassword, "active");
            if (account != null)
            {
                HttpContext.Session.SetString("Email", txtEmail);
                HttpContext.Session.SetString("Role", account.RoleId.ToString());
                Response.Redirect("/TeamPage");

            }
            else
            {
                Response.Redirect("/Login");
            }
        }
    }
}
