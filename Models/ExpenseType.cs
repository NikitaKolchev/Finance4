using System;
using System.Collections.Generic;

namespace Finance4.Models;

public partial class ExpenseType
{
    public int TypeId { get; set; }

    public string TypeName { get; set; } = null!;

    public virtual ICollection<Expense> Expenses { get; set; } = new List<Expense>();
}
