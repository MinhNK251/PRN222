using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessObjectsLayer.Models;
using RepositoriesLayer;

namespace NguyenKhanhMinhRazorPages.Pages.NewsArticlePages
{
    public class CreateModel : PageModel
    {
        private readonly INewsArticleRepo _newsArticleRepo;
        private readonly ICategoryRepo _categoryRepo;
        private readonly ISystemAccountRepo _systemAccountRepo;
        private readonly ITagRepo _tagRepo;

        public CreateModel(INewsArticleRepo newsArticleRepo, ICategoryRepo categoryRepo, ISystemAccountRepo systemAccountRepo, ITagRepo tagRepo)
        {
            _newsArticleRepo = newsArticleRepo;
            _categoryRepo = categoryRepo;
            _systemAccountRepo = systemAccountRepo;
            _tagRepo = tagRepo;
        }

        [BindProperty]
        public NewsArticle NewsArticle { get; set; } = default!;
        [BindProperty]
        public List<int> SelectedTags { get; set; } = [];

        public async Task<IActionResult> OnGetAsync()
        {
            ViewData["CategoryId"] = new SelectList(await _categoryRepo.GetCategories(), "CategoryId", "CategoryName");
            ViewData["Tags"] = new MultiSelectList(await _tagRepo.GetTags(), "TagId", "TagName");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                ViewData["CategoryId"] = new SelectList(await _categoryRepo.GetCategories(), "CategoryId", "CategoryName");
                ViewData["Tags"] = new MultiSelectList(await _tagRepo.GetTags(), "TagId", "TagName");
                return Page();
            }

            // Set Created Date and Created By
            NewsArticle.CreatedDate = DateTime.Now;
            NewsArticle.ModifiedDate = DateTime.Now;
            var currentUserEmail = HttpContext.Session.GetString("UserEmail");
            if (string.IsNullOrEmpty(currentUserEmail))
            {
                return Unauthorized();
            }
            SystemAccount account = await _systemAccountRepo.GetAccountByEmail(currentUserEmail);
            NewsArticle.CreatedById = account.AccountId;
            var existingTags = await _tagRepo.GetTagsByIds(SelectedTags);

            // Ensure EF Core does not try to insert new Tags
            foreach (var tag in existingTags)
            {
                _newsArticleRepo.AttachTag(tag); // Explicitly attach the tag to avoid re-insertion
            }

            NewsArticle.Tags = existingTags;

            _newsArticleRepo.AddNewsArticle(NewsArticle);

            return RedirectToPage("./Index");
        }
    }
}