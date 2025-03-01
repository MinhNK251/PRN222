using System;
using System.Collections.Generic;

#nullable disable

namespace Euro2024DB_BusinessObjects
{
    public partial class Team
    {
        public int Id { get; set; }
        public string TeamName { get; set; }
        public int Point { get; set; }
        public int? GroupId { get; set; }
        public int Position { get; set; }

        public virtual GroupTeam Group { get; set; }
    }
}
