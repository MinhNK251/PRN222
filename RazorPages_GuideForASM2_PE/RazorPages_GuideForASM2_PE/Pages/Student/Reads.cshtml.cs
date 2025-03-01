using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPagesSample.ServiceLayers;
using RazorPagesSample.ServiceLayers.DTOs;

namespace RazorPagesSample.Pages.Student
{
    public class ReadsModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly StudentService _studentService;

        public StudentDTO StudentDTO { get; set; }

        public ReadsModel(ILogger<IndexModel> logger, StudentService studentService)
        {
            _logger = logger;
            _studentService = studentService;
        }

        public void OnGet()
        {
        }
    }
}
