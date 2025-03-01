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
    public class DetailsModel : PageModel
    {
        private readonly RazorPagesScraefflordSample.Data.RazorPagesScraefflordSampleContext _context;

        public DetailsModel(RazorPagesScraefflordSample.Data.RazorPagesScraefflordSampleContext context)
        {
            _context = context;
        }

        public Student Student { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Student.FirstOrDefaultAsync(m => m.Id == id);
            if (student == null)
            {
                return NotFound();
            }
            else
            {
                Student = student;
            }
            return Page();
        }
    }
}
