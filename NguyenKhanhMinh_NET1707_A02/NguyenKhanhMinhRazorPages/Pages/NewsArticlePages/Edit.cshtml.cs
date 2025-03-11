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

namespace NguyenKhanhMinhRazorPages.Pages.NewsArticlePages
{
    public class EditModel : PageModel
    {
        private readonly INewsArticleRepo _newsArticleRepo;
        private readonly ICategoryRepo _categoryRepo;
        private readonly ISystemAccountRepo _systemAccountRepo;
        private readonly ITagRepo _tagRepo;

        public EditModel(INewsArticleRepo newsArticleRepo, ICategoryRepo categoryRepo, ISystemAccountRepo systemAccountRepo, ITagRepo tagRepo)
        {
            _newsArticleRepo = newsArticleRepo;
            _categoryRepo = categoryRepo;
            _systemAccountRepo = systemAccountRepo;
            _tagRepo = tagRepo;
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

            var newsarticle =  await _newsArticleRepo.GetNewsArticleById(id);
            if (newsarticle == null)
            {
                return NotFound();
            }
            NewsArticle = newsarticle;
            SelectedTags = newsarticle.Tags.Select(t => t.TagId).ToList();
            ViewData["CategoryId"] = new SelectList(await _categoryRepo.GetCategories(), "CategoryId", "CategoryName");
            ViewData["CreatedById"] = new SelectList(await _systemAccountRepo.GetAccounts(), "AccountId", "AccountName");
            ViewData["Tags"] = new MultiSelectList(await _tagRepo.GetTags(), "TagId", "TagName");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                ViewData["CategoryId"] = new SelectList(await _categoryRepo.GetCategories(), "CategoryId", "CategoryName");
                ViewData["CreatedById"] = new SelectList(await _systemAccountRepo.GetAccounts(), "AccountId", "AccountName");
                ViewData["Tags"] = new MultiSelectList(await _tagRepo.GetTags(), "TagId", "TagName");
                return Page();
            }
            try
            {
                var existingArticle = await _newsArticleRepo.GetNewsArticleById(NewsArticle.NewsArticleId);
                if (existingArticle == null)
                {
                    return NotFound();
                }

                NewsArticle.CreatedDate = existingArticle.CreatedDate;
                NewsArticle.CreatedById = existingArticle.CreatedById;
                NewsArticle.ModifiedDate = DateTime.Now;

                var currentUserEmail = HttpContext.Session.GetString("UserEmail");
                if (string.IsNullOrEmpty(currentUserEmail))
                {
                    return Unauthorized();
                }
                SystemAccount account = await _systemAccountRepo.GetAccountByEmail(currentUserEmail);
                NewsArticle.UpdatedById = account.AccountId;
                await _newsArticleRepo.RemoveTagsByArticleId(NewsArticle.NewsArticleId);
                NewsArticle.Tags = await _tagRepo.GetTagsByIds(SelectedTags);
                await _newsArticleRepo.UpdateNewsArticle(NewsArticle.NewsArticleId, NewsArticle);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (await _newsArticleRepo.GetNewsArticleById(NewsArticle.NewsArticleId) == null)
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
