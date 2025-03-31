using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BOs.Models;
using BusinessObjectLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Repositories;

namespace LionPetManagement_NguyenKhanhMinh.Pages.LionProfilePage
{
    public class EditModel : PageModel
    {
        private readonly ILionProfileRepository _lionProfileRepository;
        private readonly ILionTypeRepository _lionTypeRepository;
        private readonly IHubContext<SignalrServer> _hubContext;

        public EditModel(ILionProfileRepository lionProfileRepository, ILionTypeRepository lionTypeRepository, IHubContext<SignalrServer> hubContext)
        {
            _lionProfileRepository = lionProfileRepository;
            _lionTypeRepository = lionTypeRepository;
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
            LionProfile = lionProfile;
            ViewData["LionTypeId"] = new SelectList(_lionTypeRepository.GetLionTypes(), "LionTypeId", "LionTypeName");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                ViewData["LionTypeId"] = new SelectList(_lionTypeRepository.GetLionTypes(), "LionTypeId", "LionTypeName");
                return Page();
            }
            try
            {
                _lionProfileRepository.UpdateLionProfile(LionProfile.LionProfileId, LionProfile);
                await _hubContext.Clients.All.SendAsync("LoadData");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (_lionProfileRepository.GetLionProfileById(LionProfile.LionProfileId) == null)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }
    }
}
