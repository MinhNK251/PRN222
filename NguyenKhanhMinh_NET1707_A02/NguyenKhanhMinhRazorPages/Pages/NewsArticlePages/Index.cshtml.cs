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
using Microsoft.AspNetCore.SignalR;
using System.Text.Json.Serialization;
using System.Text.Json;

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
        [BindProperty(SupportsGet = true)]
        public string? SearchTitle { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var userRole = HttpContext.Session.GetString("UserRole");
            var articles = string.IsNullOrEmpty(userRole) || userRole.Equals("2")
                ? _newsArticleRepo.GetActiveNewsArticles()
                : _newsArticleRepo.GetNewsArticles();
            NewsArticle = articles;
            return Page();
        }

        public JsonResult OnGetLoadData(string? searchTitle)
        {
            var userRole = HttpContext.Session.GetString("UserRole");
            var articles = string.IsNullOrEmpty(userRole) || userRole.Equals("2")
                ? _newsArticleRepo.GetActiveNewsArticles()
                : _newsArticleRepo.GetNewsArticles();
            if (!string.IsNullOrEmpty(searchTitle))
            {
                articles = articles.Where(a => a.NewsTitle != null &&
                                a.NewsTitle.Contains(searchTitle, StringComparison.CurrentCultureIgnoreCase))
                    .ToList();
            }
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve, // Handle circular references
                WriteIndented = true // Optional: Makes the JSON output more readable
            };

            return new JsonResult(new { articles }, options);
        }
    }
}
