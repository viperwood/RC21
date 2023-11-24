using System;
using System.Collections.Generic;

namespace RC21.Models;

public partial class Usertable
{
    public int Id { get; set; }

    public int? Roleid { get; set; }

    public string? Login { get; set; }

    public string? Password { get; set; }

    public string? Fullname { get; set; }

    public string? Ua { get; set; }

    public string? Guid { get; set; }

    public DateTime? Releasedate { get; set; }

    public DateTime? Datasave { get; set; }

    public virtual ICollection<Accountant> Accountants { get; set; } = new List<Accountant>();

    public virtual ICollection<Admintable> Admintables { get; set; } = new List<Admintable>();

    public virtual ICollection<Laboratoryassistant> Laboratoryassistants { get; set; } = new List<Laboratoryassistant>();

    public virtual ICollection<Patient> Patients { get; set; } = new List<Patient>();

    public virtual ICollection<Releasedate> Releasedates { get; set; } = new List<Releasedate>();

    public virtual Roletable? Role { get; set; }
}
