using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorPagesCRUDSampleWithSignalR;
using RazorPagesCRUDSampleWithSignalR.Data;

namespace RazorPagesCRUDSampleWithSignalR.Pages
{
    public class DetailsModel : PageModel
    {
        private readonly RazorPagesCRUDSampleWithSignalR.Data.RazorPagesCRUDSampleWithSignalRContext _context;

        public DetailsModel(RazorPagesCRUDSampleWithSignalR.Data.RazorPagesCRUDSampleWithSignalRContext context)
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
