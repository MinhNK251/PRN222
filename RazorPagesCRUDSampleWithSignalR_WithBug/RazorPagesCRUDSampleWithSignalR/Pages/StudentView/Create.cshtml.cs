using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;

namespace RazorPagesCRUDSampleWithSignalR.Pages
{
    public class CreateModel : PageModel
    {
        private readonly RazorPagesCRUDSampleWithSignalR.Data.RazorPagesCRUDSampleWithSignalRContext _context;
        private readonly IHubContext<SignalRServer> _signalServer;

        public CreateModel(RazorPagesCRUDSampleWithSignalR.Data.RazorPagesCRUDSampleWithSignalRContext context, IHubContext<SignalRServer> signalServer)
        {
            _context = context;
            _signalServer = signalServer;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Student Student { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Student.Add(Student);
            await _context.SaveChangesAsync();
            await _signalServer.Clients.All.SendAsync("LoadStudents");

            return RedirectToPage("./Index");
        }
    }
}
