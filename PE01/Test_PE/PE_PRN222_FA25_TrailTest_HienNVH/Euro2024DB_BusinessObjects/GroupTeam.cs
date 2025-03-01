using System;
using System.Collections.Generic;

#nullable disable

namespace Euro2024DB_BusinessObjects
{
    public partial class GroupTeam
    {
        public GroupTeam()
        {
            Teams = new HashSet<Team>();
        }

        public int GroupId { get; set; }
        public string GroupName { get; set; }

        public virtual ICollection<Team> Teams { get; set; }
    }
}
