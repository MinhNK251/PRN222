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
    public class IndexModel : PageModel
    {
        private readonly ITeamRepository _teamRepository;
        private readonly IGroupTeamRepository _groupTeamRepository;

        public IndexModel(ITeamRepository teamRepository, IGroupTeamRepository groupTeamRepository)
        {
            _teamRepository = teamRepository;
            _groupTeamRepository = groupTeamRepository;
        }

        public IList<Team> Team { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Team = _teamRepository.GetTeams();
        }
    }
}
