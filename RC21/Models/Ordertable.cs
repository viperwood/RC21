using System;
using System.Collections.Generic;

namespace RC21.Models;

public partial class Ordertable
{
    public int Id { get; set; }

    public DateTime? Datecreate { get; set; }

    public decimal? Resultorder { get; set; }

    public bool? Orderstatus { get; set; }

    public string? Servicestatus { get; set; }

    public DateTime? Leadtime { get; set; }

    public int? Accountantid { get; set; }

    public int? Serviceid { get; set; }

    public int? Patientid { get; set; }

    public DateTime? Datasave { get; set; }

    public virtual Accountant? Accountant { get; set; }

    public virtual Patient? Patient { get; set; }

    public virtual Service? Service { get; set; }
}
