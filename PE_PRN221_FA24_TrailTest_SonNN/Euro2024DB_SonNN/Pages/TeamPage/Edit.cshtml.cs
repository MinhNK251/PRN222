using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Euro2024DB_BusinessObjects;
using Euro2024DB_DAO;
using Euro2024DB_Repository;

namespace Euro2024DB_SonNN.Pages.TeamPage
{
    public class EditModel : PageModel
    {
        private readonly ITeamRepository _teamRepository;
        private readonly IGroupTeamRepository _groupTeamRepository;

        public EditModel(ITeamRepository teamRepository, IGroupTeamRepository groupTeamRepository)
        {
            _teamRepository = teamRepository;
            _groupTeamRepository = groupTeamRepository;
        }

        [BindProperty]
        public Team Team { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var team =  _teamRepository.GetTeamById(id);
            if (team == null)
            {
                return NotFound();
            }
            Team = team;
           ViewData["GroupId"] = new SelectList(_groupTeamRepository.GetTeams(), "GroupId", "GroupId");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            try
            {
                _teamRepository.UpdateTeam(Team.Id, Team);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (_teamRepository.GetTeamById(Team.Id) == null)
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
