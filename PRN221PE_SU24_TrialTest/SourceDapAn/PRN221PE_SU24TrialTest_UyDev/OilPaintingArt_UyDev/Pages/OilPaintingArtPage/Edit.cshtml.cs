using BOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Repos;

namespace OilPaintingArt_UyDev.Pages.OilPaintingArtPage
{
    public class EditModel : PageModel
    {
        private readonly IArtRepo _artRepo;
        private readonly ISupplierRepo _supplierRepo;

        public EditModel(IArtRepo artRepo, ISupplierRepo supplierRepo)
        {
            _artRepo = artRepo;
            _supplierRepo = supplierRepo;
        }

        [BindProperty]
        public OilPaintingArt OilPaintingArt { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var oilpaintingart = await _artRepo.GetOilPaintingArtById(id ?? default(int));
            if (oilpaintingart == null)
            {
                return NotFound();
            }
            OilPaintingArt = oilpaintingart;

            var list = await _supplierRepo.GetList();

            ViewData["SupplierId"] = new SelectList(list, "SupplierId", "CompanyName");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {

            try
            {
                await _artRepo.UpdatePainting(OilPaintingArt);
                TempData["Message"] = "Update Succesfull";

                return RedirectToPage("./Index");
            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
                return Page();
            }
        }
    }
}
