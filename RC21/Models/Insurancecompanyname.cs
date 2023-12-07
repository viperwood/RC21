using System;
using System.Collections.Generic;

namespace RC21.Models;

public partial class Insurancecompanyname
{
    public int Id { get; set; }

    public string? Companiname { get; set; }

    public DateTime? Datasave { get; set; }

    public virtual ICollection<Insurancecompany> Insurancecompanies { get; set; } = new List<Insurancecompany>();
}
