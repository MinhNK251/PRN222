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

namespace NguyenKhanhMinhRazorPages.Pages.CategoryPages
{
    public class DeleteModel : PageModel
    {
        private readonly ICategoryRepo _categoryRepo;

        public DeleteModel(ICategoryRepo categoryRepo)
        {
            _categoryRepo = categoryRepo;
        }

        [BindProperty]
        public Category Category { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(short id)
        {
            var category = _categoryRepo.GetCategoryById(id);
            if (category == null)
            {
                return NotFound();
            }
            Category = category;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(short id)
        {
            var category = _categoryRepo.GetCategoryById(id);
            if (category != null)
            {
                if (category.NewsArticles.Any())
                {
                    TempData["ErrorMessage"] = "Cannot delete this category because it is associated with news articles.";
                    return Page();
                }
                _categoryRepo.RemoveCategory(id);
            }
            return RedirectToPage("./Index");
        }
    }
}
