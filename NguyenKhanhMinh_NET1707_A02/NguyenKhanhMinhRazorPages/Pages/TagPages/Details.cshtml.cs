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

namespace NguyenKhanhMinhRazorPages.Pages.TagPages
{
    public class DetailsModel : PageModel
    {
        private readonly ITagRepo _tagRepo;
        private readonly INewsArticleRepo _newsArticleRepo;

        public DetailsModel(ITagRepo tagRepo, INewsArticleRepo newsArticleRepo)
        {
            _tagRepo = tagRepo;
            _newsArticleRepo = newsArticleRepo;
        }

        public Tag Tag { get; set; } = default!;
        //public List<NewsArticle> NewsArticles { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var tag = _tagRepo.GetTagById(id);
            if (tag == null)
            {
                return NotFound();
            }
            Tag = tag;
            //NewsArticles = _newsArticleRepo.GetArticlesByTagId(id);
            return Page();
        }
    }
}
