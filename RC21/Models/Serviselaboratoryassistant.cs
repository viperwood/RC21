using System;
using System.Collections.Generic;

namespace RC21.Models;

public partial class Serviselaboratoryassistant
{
    public int Id { get; set; }

    public string? Nameservise { get; set; }

    public DateTime? Datasave { get; set; }

    public virtual ICollection<Laboratoryassistant> Laboratoryassistants { get; set; } = new List<Laboratoryassistant>();
}
