using System;
using System.Collections.Generic;

namespace RC21.Models;

public partial class Orderservice
{
    public int Id { get; set; }

    public int? Ordertableid { get; set; }

    public int? Serviceid { get; set; }

    public int? Cheackid { get; set; }

    public DateTime? Datasave { get; set; }

    public virtual Cheack? Cheack { get; set; }

    public virtual Ordertable? Ordertable { get; set; }

    public virtual Service? Service { get; set; }
}
