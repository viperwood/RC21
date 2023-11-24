using System;
using System.Collections.Generic;

namespace RC21.Models;

public partial class Admintable
{
    public int Id { get; set; }

    public int? Userid { get; set; }

    public DateTime? Datasave { get; set; }

    public virtual Usertable? User { get; set; }
}
