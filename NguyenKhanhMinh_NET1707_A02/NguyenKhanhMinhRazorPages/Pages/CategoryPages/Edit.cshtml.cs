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

namespace NguyenKhanhMinhRazorPages.Pages.CategoryPages
{
    public class EditModel : PageModel
    {
        private readonly ICategoryRepo _categoryRepo;

        public EditModel(ICategoryRepo categoryRepo)
        {
            _categoryRepo = categoryRepo;
        }

        [BindProperty]
        public Category Category { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(short id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = _categoryRepo.GetCategoryById(id);
            if (category == null)
            {
                return NotFound();
            }
            Category = category;
            ViewData["ParentCategoryId"] = new SelectList(_categoryRepo.GetCategories(), "CategoryId", "CategoryName");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                ViewData["ParentCategoryId"] = new SelectList(_categoryRepo.GetCategories(), "CategoryId", "CategoryName");
                return Page();
            }

            try
            {
                _categoryRepo.UpdateCategory(Category.CategoryId, Category);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (_categoryRepo.GetCategoryById(Category.CategoryId) == null)
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
