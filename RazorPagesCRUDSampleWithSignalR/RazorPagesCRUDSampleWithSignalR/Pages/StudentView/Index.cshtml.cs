using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorPagesCRUDSampleWithSignalR;
using RazorPagesCRUDSampleWithSignalR.Data;

namespace RazorPagesCRUDSampleWithSignalR.Pages.StudentView
{
    public class IndexModel : PageModel
    {
        private readonly RazorPagesCRUDSampleWithSignalR.Data.RazorPagesCRUDSampleWithSignalRContext _context;

        public IndexModel(RazorPagesCRUDSampleWithSignalR.Data.RazorPagesCRUDSampleWithSignalRContext context)
        {
            _context = context;
        }

        public IList<Student> Student { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Student = await _context.Student.ToListAsync();
        }
        public JsonResult OnGetStudents()
        {
            return new JsonResult(_context.Student);
        }
    }
}
