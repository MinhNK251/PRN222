using System;
using System.Collections.Generic;

namespace Euro2024DB_BusinessObjects;

public partial class Account
{
    public string Email { get; set; } = null!;

    public string? UserName { get; set; }

    public string? Password { get; set; }

    public string? FullName { get; set; }

    public DateTime? Birthday { get; set; }

    public string? Status { get; set; }

    public int? RoleId { get; set; }
}
