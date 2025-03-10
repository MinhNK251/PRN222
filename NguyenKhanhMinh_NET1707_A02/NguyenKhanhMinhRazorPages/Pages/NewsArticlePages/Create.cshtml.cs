using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessObjectsLayer.Models;
using DAOsLayer;

namespace NguyenKhanhMinhRazorPages.Pages.NewsArticlePages
{
    public class CreateModel : PageModel
    {
        private readonly DAOsLayer.FunewsManagementContext _context;

        public CreateModel(DAOsLayer.FunewsManagementContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryDesciption");
        ViewData["CreatedById"] = new SelectList(_context.SystemAccounts, "AccountId", "AccountId");
            return Page();
        }

        [BindProperty]
        public NewsArticle NewsArticle { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.NewsArticles.Add(NewsArticle);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
