using System;
using System.Collections.Generic;

namespace AMDT.API.Models.Entities;

public partial class UserDetail
{
    public int UserId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string DateOfBirth { get; set; } = null!;

    public int RoleType { get; set; }

    public int Status { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime ModifiedAt { get; set; }

    public virtual RoleType RoleTypeNavigation { get; set; } = null!;

    public virtual Status StatusNavigation { get; set; } = null!;
}
