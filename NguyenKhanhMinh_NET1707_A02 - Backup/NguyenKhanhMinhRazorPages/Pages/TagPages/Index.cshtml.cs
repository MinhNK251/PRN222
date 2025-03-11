using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObjectsLayer.Models;
using DAOsLayer;

namespace NguyenKhanhMinhRazorPages.Pages.TagPages
{
    public class IndexModel : PageModel
    {
        private readonly DAOsLayer.FunewsManagementContext _context;

        public IndexModel(DAOsLayer.FunewsManagementContext context)
        {
            _context = context;
        }

        public IList<Tag> Tag { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Tag = await _context.Tags.ToListAsync();
        }
    }
}
