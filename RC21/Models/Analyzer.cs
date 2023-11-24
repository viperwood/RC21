using System;
using System.Collections.Generic;

namespace RC21.Models;

public partial class Analyzer
{
    public int Id { get; set; }

    public string? Nameanalyzer { get; set; }

    public DateTime? Deadline { get; set; }

    public int? Darcode { get; set; }

    public string? Whouse { get; set; }

    public DateTime? Orderdate { get; set; }

    public int? Laboratoryassistantsid { get; set; }

    public DateTime? Datasave { get; set; }

    public virtual Laboratoryassistant? Laboratoryassistants { get; set; }

    public virtual ICollection<Service> Services { get; set; } = new List<Service>();
}
