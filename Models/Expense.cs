using System;
using System.Collections.Generic;

namespace Finance4.Models;

public partial class Expense
{
    public int ExpenseId { get; set; }

    public string Name { get; set; } = null!;

    public int? TypeId { get; set; }

    public int? PeriodId { get; set; }

    public DateOnly Date { get; set; }

    public decimal Amount { get; set; }

    public virtual BudgetPeriod? Period { get; set; }

    public virtual ExpenseType? Type { get; set; }
}
