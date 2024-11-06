using System;
using System.Collections.Generic;

namespace Finance4;

public partial class VwUserBudget
{
    public int UserId { get; set; }

    public string UserLogin { get; set; } = null!;

    public decimal Budget { get; set; }

    public decimal NeededBalance { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }
}
