using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPagesSample.ServiceLayers;
using RazorPagesSample.ServiceLayers.DTOs;

namespace RazorPagesSample.Pages.Student
{
    public class AjaxPartialModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly StudentService _studentService;

        public StudentDTO StudentDTO { get; set; }

        public AjaxPartialModel(ILogger<IndexModel> logger, StudentService studentService)
        {
            _logger = logger;
            _studentService = studentService;
        }

        public void OnGet()
        {
        }

        public PartialViewResult OnGetStudentPartial()
        {
            return Partial("_StudentPartial", _studentService.Read(1));
        }
    }
}
