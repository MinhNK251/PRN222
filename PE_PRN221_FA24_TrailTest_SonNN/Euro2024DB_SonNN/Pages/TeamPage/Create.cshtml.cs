using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Euro2024DB_BusinessObjects;
using Euro2024DB_DAO;
using Euro2024DB_Repository;

namespace Euro2024DB_SonNN.Pages.TeamPage
{
    public class CreateModel : PageModel
    {
        private readonly ITeamRepository _teamRepository;
        private readonly IGroupTeamRepository _groupTeamRepository;

        public CreateModel(ITeamRepository teamRepository, IGroupTeamRepository groupTeamRepository)
        {
            _teamRepository = teamRepository;
            _groupTeamRepository = groupTeamRepository;
        }

        public IActionResult OnGet()
        {
            ViewData["GroupId"] = new SelectList(_groupTeamRepository.GetTeams(), "GroupId", "GroupId");
            return Page();
        }

        [BindProperty]
        public Team Team { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _teamRepository.AddTeam(Team);

            return RedirectToPage("./Index");
        }
    }
}
