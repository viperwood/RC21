using System;
using System.Collections.Generic;

namespace RC21.Models;

public partial class Analizertipe
{
    public int Id { get; set; }

    public string? Analizername { get; set; }

    public DateTime? Datasave { get; set; }

    public virtual ICollection<Analyzer> Analyzers { get; set; } = new List<Analyzer>();
}
