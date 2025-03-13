using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sp25PharmaceuticalDB_BusinessObjects;
using Sp25PharmaceuticalDB_Repository;
using System.Security.Principal;

namespace PharmaceuticalManagement_SonNN.Pages
{
    public class LoginModel : PageModel
    {
        private IStoreAccountRepository _storeAccountRepository;

        public LoginModel(IStoreAccountRepository storeAccountRepository)
        {
            _storeAccountRepository = storeAccountRepository;
        }
        public void OnGet()
        {
        }

        public void OnPost()
        {
            String txtEmail = Request.Form["txtEmail"];
            String txtPassword = Request.Form["txtPassword"];
            StoreAccount account = _storeAccountRepository.GetAccount(txtEmail, txtPassword);
            if (account != null)
            {
                HttpContext.Session.SetString("Email", txtEmail);
                HttpContext.Session.SetString("Role", account.Role.ToString());
                Response.Redirect("/MedicineInformationPage");

            }
            else
            {
                Response.Redirect("/Login");
            }
        }
    }
}
