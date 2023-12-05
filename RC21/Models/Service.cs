using System;
using System.Collections.Generic;

namespace RC21.Models;

public partial class Service
{
    public int Id { get; set; }

    public int? Nameservice { get; set; }

    public DateTime? Deadline { get; set; }

    public decimal? Averagedeviation { get; set; }

    public DateTime? Dateuse { get; set; }

    public int? Analyzerid { get; set; }

    public DateTime? Datasave { get; set; }

    public virtual Analyzer? Analyzer { get; set; }

    public virtual Servicetipe? NameserviceNavigation { get; set; }

    public virtual ICollection<Orderservice> Orderservices { get; set; } = new List<Orderservice>();
}
