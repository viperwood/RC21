using System;
using System.Collections.Generic;

namespace RC21.Models;

public partial class Analyzer
{
    public int Id { get; set; }

    public int? Nameanalyzer { get; set; }

    public DateTime? Deadline { get; set; }

    public int? Barcode { get; set; }

    public string? Whouse { get; set; }

    public DateTime? Orderdate { get; set; }

    public int? Laboratoryassistantsid { get; set; }

    public DateTime? Datasave { get; set; }

    public virtual Laboratoryassistant? Laboratoryassistants { get; set; }

    public virtual Analizertipe? NameanalyzerNavigation { get; set; }

    public virtual ICollection<Service> Services { get; set; } = new List<Service>();
}
