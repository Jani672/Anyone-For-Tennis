using System;
using System.Collections.Generic;

namespace DbFirstEF.Models;

public partial class Schedule
{
    public int ScheduleId { get; set; }

    public string Name { get; set; } = null!;

    public string? Location { get; set; }

    public string? Description { get; set; }
}
