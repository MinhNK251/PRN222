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
    public class IndexModel : PageModel
    {
        private readonly ISystemAccountRepo _systemAccountRepo;

        public IndexModel(ISystemAccountRepo systemAccountRepo)
        {
            _systemAccountRepo = systemAccountRepo;
        }

        public IList<SystemAccount> SystemAccount { get;set; } = default!;

        public async Task OnGetAsync()
        {
            SystemAccount = _systemAccountRepo.GetAccounts();
        }
    }
}
