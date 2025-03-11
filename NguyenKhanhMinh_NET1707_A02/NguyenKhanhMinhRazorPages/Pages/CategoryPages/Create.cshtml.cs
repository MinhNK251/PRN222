using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessObjectsLayer.Models;
using DAOsLayer;
using RepositoriesLayer;

namespace NguyenKhanhMinhRazorPages.Pages.CategoryPages
{
    public class CreateModel : PageModel
    {
        private readonly ICategoryRepo _categoryRepo;

        public CreateModel(ICategoryRepo categoryRepo)
        {
            _categoryRepo = categoryRepo;
        }

        public IActionResult OnGet()
        {
            ViewData["ParentCategoryId"] = new SelectList(_categoryRepo.GetCategories(), "CategoryId", "CategoryName");
            return Page();
        }

        [BindProperty]
        public Category Category { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                ViewData["ParentCategoryId"] = new SelectList(_categoryRepo.GetCategories(), "CategoryId", "CategoryName");
                return Page();
            }

            _categoryRepo.AddCategory(Category);

            return RedirectToPage("./Index");
        }
    }
}
