using System;
using System.Collections.Generic;

namespace Finance4;

public partial class VwExpenseDetail
{
    public int ExpenseId { get; set; }

    public string ExpenseName { get; set; } = null!;

    public DateOnly ExpenseDate { get; set; }

    public decimal Amount { get; set; }

    public string ExpenseType { get; set; } = null!;

    public int? UserId { get; set; }

    public string UserLogin { get; set; } = null!;

    public DateOnly PeriodStartDate { get; set; }

    public DateOnly PeriodEndDate { get; set; }
}
