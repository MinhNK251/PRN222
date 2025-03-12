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
    public class IndexModel : PageModel
    {
        private readonly INewsArticleRepo _newsArticleRepo;
        private readonly ISystemAccountRepo _systemAccountRepo;

        public IndexModel(INewsArticleRepo newsArticleRepo, ISystemAccountRepo systemAccountRepo)
        {
            _newsArticleRepo = newsArticleRepo;
            _systemAccountRepo = systemAccountRepo;
        }

        public IList<NewsArticle> NewsArticle { get;set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            var userRole = HttpContext.Session.GetString("UserRole");
            if (userRole.Equals("2"))
                NewsArticle = _newsArticleRepo.GetActiveNewsArticles();
            else NewsArticle = _newsArticleRepo.GetNewsArticles();
            return Page();
        }
    }
}
