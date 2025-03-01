using BOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repos;

namespace OilPaintingArt_UyDev.Pages.OilPaintingArtPage
{
    public class IndexModel : PageModel
    {

        //search
        [BindProperty(SupportsGet = true)]
        public string searchTerm { get; set; }

        //paging
        public int PageIndex { get; set; } = 1;

        public int TotalPages { get; set; }
        public int PageSize { get; set; } = 2;

        private readonly IArtRepo _artRepo;

        public IndexModel(IArtRepo artRepo)
        {
            _artRepo = artRepo;
        }

        public IList<OilPaintingArt> OilPaintingArt { get; set; } = default!;

        public async Task OnGetAsync(int pageIndex = 1)
        {
            var result = await _artRepo.GetList(searchTerm, pageIndex, 2);

            OilPaintingArt = result.OilPaintingArts;
            PageIndex = result.PageIndex;
            TotalPages = result.TotalPages;
        }
    }
}
