using Azure;
using BOs.Models;
using BusinessObjectLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repositories;
using System.Security.Principal;

namespace LionPetManagement_NguyenKhanhMinh.Pages
{
    public class LoginModel : PageModel
    {
        private ILionAccountRepository _lionAccountRepository;

        public LoginModel(ILionAccountRepository lionAccountRepository)
        {
            _lionAccountRepository = lionAccountRepository;
        }

        public string? ErrorMessage { get; set; }

        public void OnGet()
        {
        }

        public void OnPost()
        {
            String txtEmail = Request.Form["txtEmail"];
            String txtPassword = Request.Form["txtPassword"];
            LionAccount account = _lionAccountRepository.GetAccount(txtEmail, txtPassword);
            if (account != null)
            {
                HttpContext.Session.SetString("Email", txtEmail);
                HttpContext.Session.SetString("Role", account.RoleId.ToString());
                Response.Redirect("/LionProfilePage");

            }
            else
            {
                ErrorMessage = "You do not have permission to do this function!";
                RedirectToPage();
            }
        }
    }
}
