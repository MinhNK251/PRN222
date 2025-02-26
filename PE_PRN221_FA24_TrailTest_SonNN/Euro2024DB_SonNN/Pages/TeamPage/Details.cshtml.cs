using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Euro2024DB_BusinessObjects;
using Euro2024DB_DAO;
using Euro2024DB_Repository;

namespace Euro2024DB_SonNN.Pages.TeamPage
{
    public class DetailsModel : PageModel
    {
        private readonly ITeamRepository _teamRepository;

        public DetailsModel(ITeamRepository teamRepository)
        {
            _teamRepository = teamRepository;
        }

        public Team Team { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var team = _teamRepository.GetTeamById(id);
            if (team == null)
            {
                return NotFound();
            }
            else
            {
                Team = team;
            }
            return Page();
        }
    }
}
