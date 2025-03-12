using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BusinessObjectsLayer.Models;
using DAOsLayer;
using RepositoriesLayer;

namespace NguyenKhanhMinhRazorPages.Pages.SystemAccountPages
{
    public class EditModel : PageModel
    {
        private readonly ISystemAccountRepo _systemAccountRepo;

        public EditModel(ISystemAccountRepo systemAccountRepo)
        {
            _systemAccountRepo = systemAccountRepo;
        }

        [BindProperty]
        public SystemAccount SystemAccount { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(short id)
        {
            var systemaccount = _systemAccountRepo.GetAccountById(id);
            if (systemaccount == null)
            {
                return NotFound();
            }
            SystemAccount = systemaccount;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
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

            return RedirectToPage("./Index");
        }
    }
}
