using System;
using System.Collections.Generic;

namespace api_docmanager.Entities;

public partial class UserRole
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public bool? Deleted { get; set; }

    public virtual ICollection<UserAccount> UserAccounts { get; set; } = new List<UserAccount>();
}
