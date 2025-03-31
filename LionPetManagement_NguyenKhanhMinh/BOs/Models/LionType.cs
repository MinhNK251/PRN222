using System;
using System.Collections.Generic;

namespace BOs.Models;

public partial class LionType
{
    public int LionTypeId { get; set; }

    public string? LionTypeName { get; set; }

    public string? Origin { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<LionProfile> LionProfiles { get; set; } = new List<LionProfile>();
}
