using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObjectsLayer.Models;
using DAOsLayer;
using RepositoriesLayer;

namespace NguyenKhanhMinhRazorPages.Pages.SystemAccountPages
{
    public class DetailsModel : PageModel
    {
        private readonly ISystemAccountRepo _systemAccountRepo;

        public DetailsModel(ISystemAccountRepo systemAccountRepo)
        {
            _systemAccountRepo = systemAccountRepo;
        }

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
    }
}
