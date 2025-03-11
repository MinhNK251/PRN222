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

namespace NguyenKhanhMinhRazorPages.Pages.TagPages
{
    public class IndexModel : PageModel
    {
        private readonly ITagRepo _tagRepo;

        public IndexModel(ITagRepo tagRepo)
        {
            _tagRepo = tagRepo;
        }

        public IList<Tag> Tag { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Tag = _tagRepo.GetTags();
        }
    }
}
