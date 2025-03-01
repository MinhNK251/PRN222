using BOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Repos;

namespace OilPaintingArt_UyDev.Pages.OilPaintingArtPage
{
    public class CreateModel : PageModel
    {
        private readonly IArtRepo _artRepo;
        private readonly ISupplierRepo _supplierRepo;

        public CreateModel(IArtRepo artRepo, ISupplierRepo supplierRepo)
        {
            _artRepo = artRepo;
            _supplierRepo = supplierRepo;
        }

        public async Task<IActionResult> OnGet()
        {
            var listItem = await _supplierRepo.GetList();
            ViewData["SupplierId"] = new SelectList(listItem, "SupplierId", "CompanyName");
            return Page();
        }

        [BindProperty]
        public OilPaintingArt OilPaintingArt { get; set; } = default!;


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                await _artRepo.AddPainting(OilPaintingArt);
                TempData["Message"] = "Create Succesfull";
                return RedirectToPage("./Index");
            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
                return await OnGet();
            }
        }
    }
}
