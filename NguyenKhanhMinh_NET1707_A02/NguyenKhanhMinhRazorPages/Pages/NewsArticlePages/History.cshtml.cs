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

namespace NguyenKhanhMinhRazorPages.Pages.NewsArticlePages
{
    public class HistoryModel : PageModel
    {
        private readonly INewsArticleRepo _newsArticleRepo;
        private readonly ISystemAccountRepo _systemAccountRepo;

        public HistoryModel(INewsArticleRepo newsArticleRepo, ISystemAccountRepo systemAccountRepo)
        {
            _newsArticleRepo = newsArticleRepo;
            _systemAccountRepo = systemAccountRepo;
        }

        public IList<NewsArticle> NewsArticle { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            var userEmail = HttpContext.Session.GetString("UserEmail");
            if (string.IsNullOrEmpty(userEmail))
            {
                return RedirectToPage("/NotPermission");
            }
            var currentAccount = _systemAccountRepo.GetAccountByEmail(userEmail);
            NewsArticle = _newsArticleRepo.GetNewsArticlesByCreatedBy(currentAccount.AccountId);
            return Page();
        }
    }
}
