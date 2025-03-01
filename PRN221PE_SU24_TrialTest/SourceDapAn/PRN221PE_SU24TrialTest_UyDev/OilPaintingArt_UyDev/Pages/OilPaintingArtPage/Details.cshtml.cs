using BOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repos;

namespace OilPaintingArt_UyDev.Pages.OilPaintingArtPage
{
    public class DetailsModel : PageModel
    {
        private readonly IArtRepo _artRepo;

        public DetailsModel(IArtRepo artRepo)
        {
            _artRepo = artRepo;
        }

        public OilPaintingArt OilPaintingArt { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            OilPaintingArt = await _artRepo.GetOilPaintingArtById(id ?? default(int));
            return Page();
        }
    }
}
