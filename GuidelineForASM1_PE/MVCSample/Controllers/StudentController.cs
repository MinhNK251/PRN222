using Microsoft.AspNetCore.Mvc;
using ServiceLayer;

namespace MVCSample.Controllers
{
    public class StudentController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly StudentService _studentService;

        public StudentController(ILogger<HomeController> logger, StudentService studentService)
        {
            _logger = logger;
            _studentService = studentService;
        }

        public IActionResult Read(int id)
        {
            return View(_studentService.Get(id));
        }
    }
}
