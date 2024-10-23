using System;
using System.Collections.Generic;

namespace DbFirstEF.Models;

public partial class Member
{
    public int MemberId { get; set; }

    public string? FirstName { get; set; }

    public string LastName { get; set; } = null!;

    public string? Email { get; set; }

    public bool Active { get; set; }
}
