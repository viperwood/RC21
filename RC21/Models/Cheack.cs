using System;
using System.Collections.Generic;

namespace RC21.Models;

public partial class Cheack
{
    public int Id { get; set; }

    public int? Insurancecompanyid { get; set; }

    public int? Patientid { get; set; }

    public int? Accountantid { get; set; }

    public DateTime? Datasave { get; set; }

    public virtual Accountant? Accountant { get; set; }

    public virtual Insurancecompany? Insurancecompany { get; set; }

    public virtual ICollection<Orderservice> Orderservices { get; set; } = new List<Orderservice>();

    public virtual Patient? Patient { get; set; }
}
