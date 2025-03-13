using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessObjectsLayer.Models;
using RepositoriesLayer;
using Microsoft.AspNetCore.SignalR;

namespace NguyenKhanhMinhRazorPages.Pages.NewsArticlePages
{
    public class CreateModel : PageModel
    {
        private readonly INewsArticleRepo _newsArticleRepo;
        private readonly ICategoryRepo _categoryRepo;
        private readonly ISystemAccountRepo _systemAccountRepo;
        private readonly ITagRepo _tagRepo;
        private readonly IHubContext<SignalrServer> _hubContext;

        public CreateModel(INewsArticleRepo newsArticleRepo, ICategoryRepo categoryRepo, ISystemAccountRepo systemAccountRepo, ITagRepo tagRepo, IHubContext<SignalrServer> hubContext)
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
        public List<int>? SelectedTags { get; set; } = [];

        public async Task<IActionResult> OnGetAsync()
        {
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
            var existingArticle = _newsArticleRepo.GetNewsArticleById(NewsArticle.NewsArticleId);
            if (existingArticle != null)
            {
                ModelState.AddModelError("NewsArticle.NewsArticleId", "This NewsArticle ID already exists. Please enter a unique ID.");
                ViewData["CategoryId"] = new SelectList(_categoryRepo.GetCategories(), "CategoryId", "CategoryName");
                ViewData["Tags"] = new MultiSelectList(_tagRepo.GetTags(), "TagId", "TagName");
                return Page();
            }
            NewsArticle.CreatedDate = DateTime.Now;
            NewsArticle.ModifiedDate = DateTime.Now;
            var userEmail = HttpContext.Session.GetString("UserEmail");
            if (string.IsNullOrEmpty(userEmail))
            {
                return RedirectToPage("/Login");
            }
            SystemAccount account = _systemAccountRepo.GetAccountByEmail(userEmail);
            NewsArticle.CreatedById = account.AccountId;
            NewsArticle.UpdatedById = account.AccountId;
            if (SelectedTags != null && SelectedTags.Any())
            {
                var existingTags = _tagRepo.GetTagsByIds(SelectedTags);
                NewsArticle.Tags = existingTags;
            }
            else
            {
                NewsArticle.Tags = new List<Tag>();
            }
            _newsArticleRepo.AddNewsArticle(NewsArticle);
            await _hubContext.Clients.All.SendAsync("LoadData");
            return RedirectToPage("./Index");
        }
    }
}