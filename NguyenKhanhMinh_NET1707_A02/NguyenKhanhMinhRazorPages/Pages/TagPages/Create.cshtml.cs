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

namespace NguyenKhanhMinhRazorPages.Pages.TagPages
{
    public class CreateModel : PageModel
    {
        private readonly ITagRepo _tagRepo;

        public CreateModel(ITagRepo tagRepo)
        {
            _tagRepo = tagRepo;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Tag Tag { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (_tagRepo.GetTagById(Tag.TagId) != null)
            {
                ModelState.AddModelError("Tag.TagId", "This Tag ID already exists. Please enter a unique ID.");
                return Page();
            }
            _tagRepo.AddTag(Tag);
            return RedirectToPage("./Index");
        }
    }
}
