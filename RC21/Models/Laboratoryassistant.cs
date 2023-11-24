using System;
using System.Collections.Generic;

namespace RC21.Models;

public partial class Laboratoryassistant
{
    public int Id { get; set; }

    public int? Userid { get; set; }

    public int? Serviselaboratoryassistantsid { get; set; }

    public DateTime? Datasave { get; set; }

    public virtual ICollection<Analyzer> Analyzers { get; set; } = new List<Analyzer>();

    public virtual Serviselaboratoryassistant? Serviselaboratoryassistants { get; set; }

    public virtual Usertable? User { get; set; }
}
