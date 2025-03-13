using System;
using System.Collections.Generic;

namespace Sp25PharmaceuticalDB_BusinessObjects;

public partial class StoreAccount
{
    public int StoreAccountId { get; set; }

    public string StoreAccountPassword { get; set; } = null!;

    public string? EmailAddress { get; set; }

    public string StoreAccountDescription { get; set; } = null!;

    public int? Role { get; set; }
}
