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
using Microsoft.AspNetCore.SignalR;

namespace NguyenKhanhMinhRazorPages.Pages.NewsArticlePages
{
    public class EditModel : PageModel
    {
        private readonly INewsArticleRepo _newsArticleRepo;
        private readonly ICategoryRepo _categoryRepo;
        private readonly ISystemAccountRepo _systemAccountRepo;
        private readonly ITagRepo _tagRepo;
        private readonly IHubContext<SignalrServer> _hubContext;

        public EditModel(INewsArticleRepo newsArticleRepo, ICategoryRepo categoryRepo, ISystemAccountRepo systemAccountRepo, ITagRepo tagRepo, IHubContext<SignalrServer> hubContext)
        {
            _newsArticleRepo = newsArticleRepo;
            _categoryRepo = categoryRepo;
            _systemAccountRepo = systemAccountRepo;
            _tagRepo = tagRepo;
            _hubContext = hubContext;
        }

        [BindProperty]
        public NewsArticle NewsArticle { get; set; } = default!;
        [BindProperty]
        public List<int> SelectedTags { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var newsArticle =  _newsArticleRepo.GetNewsArticleById(id);
            if (newsArticle == null)
            {
                return NotFound();
            }
            NewsArticle = newsArticle;
            SelectedTags = newsArticle.Tags.Select(t => t.TagId).ToList();
            ViewData["CategoryId"] = new SelectList(_categoryRepo.GetCategories(), "CategoryId", "CategoryName");
            ViewData["Tags"] = new MultiSelectList(_tagRepo.GetTags(), "TagId", "TagName");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                ViewData["CategoryId"] = new SelectList(_categoryRepo.GetCategories(), "CategoryId", "CategoryName");
                ViewData["Tags"] = new MultiSelectList(_tagRepo.GetTags(), "TagId", "TagName");
                return Page();
            }

            try
            {
                // ✅ Avoid reloading NewsArticle
                var existingArticle = _newsArticleRepo.GetNewsArticleById(NewsArticle.NewsArticleId);
                if (existingArticle == null)
                {
                    return NotFound();
                }
                var userEmail = HttpContext.Session.GetString("UserEmail");
                if (string.IsNullOrEmpty(userEmail))
                {
                    return RedirectToPage("/Login"); // Redirect to login if no session exists
                }
                var currentUser = _systemAccountRepo.GetAccountByEmail(userEmail);
                existingArticle.UpdatedBy = currentUser;
                existingArticle.NewsTitle = NewsArticle.NewsTitle;
                existingArticle.Headline = NewsArticle.Headline;
                existingArticle.NewsContent = NewsArticle.NewsContent;
                existingArticle.NewsSource = NewsArticle.NewsSource;
                existingArticle.CategoryId = NewsArticle.CategoryId;
                existingArticle.NewsStatus = NewsArticle.NewsStatus;
                existingArticle.ModifiedDate = DateTime.Now;
                _newsArticleRepo.RemoveTagsByArticleId(existingArticle.NewsArticleId);
                existingArticle.Tags = _tagRepo.GetTagsByIds(SelectedTags);
                _newsArticleRepo.UpdateNewsArticle(existingArticle.NewsArticleId, existingArticle);
                await _hubContext.Clients.All.SendAsync("LoadData");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (_newsArticleRepo.GetNewsArticleById(NewsArticle.NewsArticleId) == null)
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
