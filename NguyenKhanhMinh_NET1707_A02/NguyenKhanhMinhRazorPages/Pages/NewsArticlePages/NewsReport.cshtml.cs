using BusinessObjectsLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RepositoriesLayer;
using System;
using System.Collections.Generic;

namespace NguyenKhanhMinhRazorPages.Pages.NewsArticlePages
{
    public class NewsReportModel : PageModel
    {
        private readonly INewsArticleRepo _newsArticleRepo;

        public NewsReportModel(INewsArticleRepo newsArticleRepo)
        {
            _newsArticleRepo = newsArticleRepo;
        }

        [BindProperty]
        public DateTime StartDate { get; set; } = new DateTime(2024, 1, 1);

        [BindProperty]
        public DateTime EndDate { get; set; } = DateTime.Today;

        public List<NewsArticle> NewsArticles { get; set; } = new();

        public IActionResult OnGet()
        {
            NewsArticles = _newsArticleRepo.GetNewsArticlesByDateRange(StartDate, EndDate);
            return Page();
        }

        public IActionResult OnPost()
        {
            NewsArticles = _newsArticleRepo.GetNewsArticlesByDateRange(StartDate, EndDate);
            return Page();
        }
    }
}
