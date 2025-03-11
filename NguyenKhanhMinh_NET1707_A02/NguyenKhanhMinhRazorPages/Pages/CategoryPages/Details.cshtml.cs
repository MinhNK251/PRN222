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
    public class DetailsModel : PageModel
    {
        private readonly ICategoryRepo _categoryRepo;

        public DetailsModel(ICategoryRepo categoryRepo)
        {
            _categoryRepo = categoryRepo;
        }

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
    }
}
