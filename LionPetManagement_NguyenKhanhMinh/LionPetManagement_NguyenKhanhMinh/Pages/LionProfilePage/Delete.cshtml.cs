using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BOs.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Repositories;

namespace LionPetManagement_NguyenKhanhMinh.Pages.LionProfilePage
{
    public class DeleteModel : PageModel
    {
        private readonly ILionProfileRepository _lionProfileRepository;
        private readonly IHubContext<SignalrServer> _hubContext;

        public DeleteModel(ILionProfileRepository lionProfileRepository, IHubContext<SignalrServer> hubContext)
        {
            _lionProfileRepository = lionProfileRepository;
            _hubContext = hubContext;
        }

        [BindProperty]
        public LionProfile LionProfile { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lionProfile = _lionProfileRepository.GetLionProfileById(id);

            if (lionProfile == null)
            {
                return NotFound();
            }
            else
            {
                LionProfile = lionProfile;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lionProfile = _lionProfileRepository.GetLionProfileById(id);
            if (lionProfile != null)
            {
                _lionProfileRepository.RemoveLionProfile(id);
                await _hubContext.Clients.All.SendAsync("LoadData");
            }

            return RedirectToPage("./Index");
        }
    }
}
