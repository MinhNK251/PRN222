using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BOs.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.SignalR;
using Repositories;

namespace LionPetManagement_NguyenKhanhMinh.Pages.LionProfilePage
{
    public class CreateModel : PageModel
    {
        private readonly ILionProfileRepository _lionProfileRepository;
        private readonly ILionTypeRepository _lionTypeRepository;
        private readonly IHubContext<SignalrServer> _hubContext;

        public CreateModel(ILionProfileRepository lionProfileRepository, ILionTypeRepository lionTypeRepository, IHubContext<SignalrServer> hubContext)
        {
            _lionProfileRepository = lionProfileRepository;
            _lionTypeRepository = lionTypeRepository;
            _hubContext = hubContext;
        }

        public IActionResult OnGet()
        {
            ViewData["LionTypeId"] = new SelectList(_lionTypeRepository.GetLionTypes(), "LionTypeId", "LionTypeName");
            return Page();
        }

        [BindProperty]
        public LionProfile LionProfile { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                ViewData["LionTypeId"] = new SelectList(_lionTypeRepository.GetLionTypes(), "LionTypeId", "LionTypeName");
                return Page();
            }

            try
            {
                _lionProfileRepository.AddLionProfile(LionProfile);
                await _hubContext.Clients.All.SendAsync("LoadData");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred: {ex.Message}");
                ModelState.AddModelError(string.Empty, "An error occurred while creating the lion profile.");
                return Page();
            }

            return RedirectToPage("./Index");
        }


    }
}
