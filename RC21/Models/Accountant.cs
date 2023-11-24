using System;
using System.Collections.Generic;

namespace RC21.Models;

public partial class Accountant
{
    public int Id { get; set; }

    public int? Serviseaccountantid { get; set; }

    public int? Userid { get; set; }

    public int? Checkic { get; set; }

    public DateTime? Datasave { get; set; }

    public virtual ICollection<Cheack> Cheacks { get; set; } = new List<Cheack>();

    public virtual Serviseaccountant? Serviseaccountant { get; set; }

    public virtual Usertable? User { get; set; }
}
