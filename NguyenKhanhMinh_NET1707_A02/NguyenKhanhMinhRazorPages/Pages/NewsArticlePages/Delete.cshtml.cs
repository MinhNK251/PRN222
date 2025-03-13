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

namespace NguyenKhanhMinhRazorPages.Pages.NewsArticlePages
{
    public class DeleteModel : PageModel
    {
        private readonly INewsArticleRepo _newsArticleRepo;
        private readonly ITagRepo _tagRepo;
        private readonly IHubContext<SignalrServer> _hubContext;

        public DeleteModel(INewsArticleRepo newsArticleRepo, ITagRepo tagRepo, IHubContext<SignalrServer> hubContext)
        {
            _newsArticleRepo = newsArticleRepo;
            _tagRepo = tagRepo;
            _hubContext = hubContext;
        }

        public NewsArticle NewsArticle { get; set; } = default!;
        public List<Tag> Tags { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var newsArticle = _newsArticleRepo.GetNewsArticleById(id);
            if (newsArticle == null)
            {
                return NotFound();
            }

            NewsArticle = newsArticle;

            // Fetch related tags
            Tags = _tagRepo.GetTagsByNewsArticleId(id);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var existingArticle = _newsArticleRepo.GetNewsArticleById(id);
            if (existingArticle != null)
            {
                _newsArticleRepo.RemoveTagsByArticleId(id);
                _newsArticleRepo.RemoveNewsArticle(id);
                await _hubContext.Clients.All.SendAsync("LoadData");
            }

            return RedirectToPage("./Index");
        }
    }
}
