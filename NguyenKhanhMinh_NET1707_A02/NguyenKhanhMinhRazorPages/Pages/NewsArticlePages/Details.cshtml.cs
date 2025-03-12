using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BusinessObjectsLayer.Models;
using RepositoriesLayer;

namespace NguyenKhanhMinhRazorPages.Pages.NewsArticlePages
{
    public class DetailsModel : PageModel
    {
        private readonly INewsArticleRepo _newsArticleRepo;
        private readonly ITagRepo _tagRepo;

        public DetailsModel(INewsArticleRepo newsArticleRepo, ITagRepo tagRepo)
        {
            _newsArticleRepo = newsArticleRepo;
            _tagRepo = tagRepo;
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
    }
}
