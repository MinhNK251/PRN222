using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorPagesCRUDSampleWithSignalR.Pages.StudentView
{
    public class StudentListModel : PageModel
    {
        private readonly RazorPagesCRUDSampleWithSignalR.Data.RazorPagesCRUDSampleWithSignalRContext _context;
        public StudentListModel(RazorPagesCRUDSampleWithSignalR.Data.RazorPagesCRUDSampleWithSignalRContext context)
        {
            _context = context;
        }
        public void OnGet()
        {
        }

        public JsonResult OnGetStudents()
        {
            return new JsonResult(_context.Student);
        }
    }
}
