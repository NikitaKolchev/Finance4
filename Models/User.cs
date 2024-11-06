using System;
using System.Collections.Generic;

namespace Finance4.Models;

public partial class User
{
    public int UserId { get; set; }

    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Email { get; set; } = null!;

    public virtual ICollection<BudgetPeriod> BudgetPeriods { get; set; } = new List<BudgetPeriod>();
}
