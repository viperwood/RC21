using System;
using System.Collections.Generic;

namespace RC21.Models;

public partial class Insurancecompany
{
    public int Id { get; set; }

    public int? Namecompanyid { get; set; }

    public string? Address { get; set; }

    public int? Unn { get; set; }

    public int? Pc { get; set; }

    public string? Ein { get; set; }

    public string? Ipadress { get; set; }

    public decimal? Checkingaccount { get; set; }

    public int? Bic { get; set; }

    public DateTime? Datasave { get; set; }

    public virtual Insurancecompanyname? Namecompany { get; set; }

    public virtual ICollection<Ordertable> Ordertables { get; set; } = new List<Ordertable>();
}
