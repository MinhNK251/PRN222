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
    public class IndexModel : PageModel
    {
        private readonly ICategoryRepo _categoryRepo;

        public IndexModel(ICategoryRepo categoryRepo)
        {
            _categoryRepo = categoryRepo;
        }

        public IList<Category> Category { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Category = _categoryRepo.GetCategories();
        }
    }
}
