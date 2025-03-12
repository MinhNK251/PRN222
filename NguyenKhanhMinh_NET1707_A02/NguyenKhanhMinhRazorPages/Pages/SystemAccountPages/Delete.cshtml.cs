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
    public class DeleteModel : PageModel
    {
        private readonly ISystemAccountRepo _systemAccountRepo;
        private readonly INewsArticleRepo _newsArticleRepo;

        public DeleteModel(ISystemAccountRepo systemAccountRepo, INewsArticleRepo newsArticleRepo)
        {
            _systemAccountRepo = systemAccountRepo;
            _newsArticleRepo = newsArticleRepo;
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

        public async Task<IActionResult> OnPostAsync(short id)
        {
            var systemaccount = _systemAccountRepo.GetAccountById(id);
            if (systemaccount != null)
            {
                SystemAccount = systemaccount;
                foreach (var article in systemaccount.NewsArticles)
                {
                    _newsArticleRepo.RemoveTagsByArticleId(article.NewsArticleId);
                    _newsArticleRepo.RemoveNewsArticle(article.NewsArticleId);
                }
                _systemAccountRepo.RemoveAccount(id);
            }

            return RedirectToPage("./Index");
        }
    }
}
