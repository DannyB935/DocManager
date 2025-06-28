using System;
using System.Collections.Generic;

namespace api_docmanager.Entities;

public partial class Unit
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Prefix { get; set; }

    public bool? Deleted { get; set; }

    public virtual ICollection<Document> Documents { get; set; } = new List<Document>();

    public virtual ICollection<UserAccount> UserAccounts { get; set; } = new List<UserAccount>();
}
