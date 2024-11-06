using System;
using System.Collections.Generic;

namespace Finance4.Models;

public partial class BudgetPeriod
{
    public int PeriodId { get; set; }

    public int? UserId { get; set; }

    public decimal Budget { get; set; }

    public decimal NeededBalance { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public virtual ICollection<Expense> Expenses { get; set; } = new List<Expense>();

    public virtual User? User { get; set; }
}
