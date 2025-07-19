using System;
using System.Collections.Generic;

namespace AMDT.API.Models.Entities;

public partial class Status
{
    public int StatusId { get; set; }

    public string StatusName { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime ModifiedAt { get; set; }

    public virtual ICollection<RoleType> RoleTypes { get; set; } = new List<RoleType>();
}
