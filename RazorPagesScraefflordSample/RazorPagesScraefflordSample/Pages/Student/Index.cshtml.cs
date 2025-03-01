using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorPagesScraefflordSample.Data;

namespace RazorPagesScraefflordSample
{
    public class IndexModel : PageModel
    {
        private readonly RazorPagesScraefflordSample.Data.RazorPagesScraefflordSampleContext _context;

        public IndexModel(RazorPagesScraefflordSample.Data.RazorPagesScraefflordSampleContext context)
        {
            _context = context;
        }

        public IList<Student> Student { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Student = await _context.Student.ToListAsync();
        }
    }
}
