using System;
using System.Collections.Generic;

namespace AMDT.API.Models.Entities;

public partial class RoleType
{
    public int RoleId { get; set; }

    public string RoleName { get; set; } = null!;

    public int Status { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime ModifiedAt { get; set; }

    public virtual Status StatusNavigation { get; set; } = null!;
}
