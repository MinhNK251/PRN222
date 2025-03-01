using BOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repos;

namespace OilPaintingArt_UyDev.Pages.OilPaintingArtPage
{
    public class DeleteModel : PageModel
    {
        private readonly IArtRepo _artRepo;

        public DeleteModel(IArtRepo artRepo)
        {
            _artRepo = artRepo;
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
            else
            {
                OilPaintingArt = oilpaintingart;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }
                await _artRepo.DeletePainting(id ?? default(int));
                TempData["Message"] = "Delete Succesfull";

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
